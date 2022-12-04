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
        if (parent.GetItem() != "")
        {
            if (mouseHoveringOver && Input.GetMouseButtonDown(0))
            {
                beingDragged = true;
                parent.gameObject.transform.SetAsLastSibling();
            }
        } 
          
        if (Input.GetMouseButtonUp(0) && beingDragged)
        {
            GameObject slot = IsPointerOverUIElement("InventorySlot");
            if (slot != null)
            {   
                string itemName = parent.GetItem();
                string otherSlotItemName = slot.GetComponent<InventorySlot>().GetItem();

                inventory.AddItemToInventory(otherSlotItemName, parent);
                inventory.AddItemToInventory(itemName, slot.GetComponent<InventorySlot>());

                transform.localPosition = Vector2.zero;
            }
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
