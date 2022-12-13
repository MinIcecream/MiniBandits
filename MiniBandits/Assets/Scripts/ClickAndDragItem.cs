using System.Collections;
using System.Collections.Generic; 
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems; 

public class ClickAndDragItem : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    bool mouseHoveringOver = false;
    public bool beingDragged = false;
    public PlayerInventory inventory;
    public InventorySlot parent;
     

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseHoveringOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    { 
        mouseHoveringOver = false;
    }

    void Update()
    { 
        //IF THERE IS AN ITEM IN THIS SLOT:
        if (parent.GetItem() != null)
        {
            //START DRAGGIN IT.
            if (mouseHoveringOver && Input.GetMouseButtonDown(0))
            {
                beingDragged = true;
                parent.gameObject.transform.SetAsLastSibling();
            }
        } 
          
        //IF YOU WERE DRAGGING SOMETING AND LET GO:
        if (Input.GetMouseButtonUp(0) && beingDragged)
        {
            GameObject slot = IsPointerOverUIElement("InventorySlot");
            //IF YOU"RE OVER A SLOT:
            if (slot != null)
            {
                var compatibleType = slot.GetComponent<InventorySlot>().acceptedItems;
                //CHECKING IF SLOT IS COMPATIBLE..l.
                if (compatibleType == parent.GetItem().type || compatibleType == Item.itemType.basic)
                { 
                    Item item = parent.GetItem();
                    Item otherSlotItem = slot.GetComponent<InventorySlot>().GetItem();

                    inventory.AddItemToInventory(otherSlotItem, parent);
                    inventory.AddItemToInventory(item, slot.GetComponent<InventorySlot>());

                }
                transform.localPosition = Vector2.zero;
            }

            //OTHERWISE, JUST DON't DO ANYHTING
            else
            { 
                transform.localPosition = Vector2.zero;
            }
            beingDragged = false;
        }

        if (beingDragged)
        {
            transform.position = Input.mousePosition;
        }
           
    }
    public static GameObject IsPointerOverUIElement(string tag)
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach(var res in results)
        { 
            if (res.gameObject.tag == tag)
            {
                return res.gameObject;
            }
        }
        return null;
    }
}
