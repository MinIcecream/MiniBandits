using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    public PlayerInventory inven;
    public Transform spawnPt;
    public GameObject activeWeapon;

    void Awake()
    {
        inven = GameObject.FindWithTag("Inventory").GetComponent<PlayerInventory>();
        UpdateWeapon();
    }
    void OnEnable()
    {
        PlayerInventory.OnInventoryUpdate += UpdateWeapon;
    }

    void OnDisable()
    {
        PlayerInventory.OnInventoryUpdate -= UpdateWeapon;
    }

    void UpdateWeapon()
    { 
        Item weapon = inven.GetActiveWeapon();

        //IF NO WEAPON EQUIPPED:
        if (!weapon)
        {
            Debug.Log("NO WEAPON EQUIPPEED");
            Destroy(activeWeapon);
            return;
        }
        //IF ACTIVRE WEAPON IS EQUIPPED:
        if (activeWeapon)
        { 
            //IF THAT SAME WEAPON ALREADY EQUIPPED: RETURN
            if (activeWeapon.GetComponent<WeaponTemplate>().GetName() == weapon.referenceName)
            {
                return;
            }

            //IF A DIFFERENT WEAPON IS EQUIPOPPED, DESTROY IT.
            Destroy(activeWeapon);

        }

        //OTHERWISE:
        //TIME TO UPDATE THE WEAPON!  
        var weaponPrefab = Resources.Load<GameObject>("WeaponPrefabs/" + weapon.referenceName);

        if (weaponPrefab == null)
        {
            Debug.Log("WEAPON NOT IMPLEMENTED!!");
            return;
        }
         
        activeWeapon = Instantiate(weaponPrefab, spawnPt.position, Quaternion.identity);
   
        //If the weapon was successfully equiped: 
        activeWeapon.transform.SetParent(this.gameObject.transform);
        activeWeapon.transform.localRotation = Quaternion.identity;

    }
}
