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
        string weapon = inven.GetActiveWeapon();
        //IF NO ACTIVRE WEAPON:
        if (activeWeapon)
        { 
            //IF THAT WEAPON ALREADY EQUIPPED: RETURN
            if (activeWeapon.GetComponent<BaseWeapon>().weaponName == weapon)
            {
                return;
            }
            Destroy(activeWeapon);

        }
        //TIME TO UPDATE THE WEAPON!  

        var weaponPrefab = Resources.Load<GameObject>("WeaponPrefabs/" + weapon);

        if (weaponPrefab == null)
        {
            Debug.Log("WEAPON NOT IMPLEMENTED!!");
            return;
        }

        activeWeapon = Instantiate(weaponPrefab, spawnPt.position, Quaternion.identity);
   
        //If the weapon was successfully equiped: 
        activeWeapon.transform.SetParent(this.gameObject.transform);
   
    }
}
