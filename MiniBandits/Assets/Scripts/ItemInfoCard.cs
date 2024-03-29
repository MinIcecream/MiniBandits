using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInfoCard : MonoBehaviour
{
    public Image image;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    public TextMeshProUGUI itemStats;

    public void DisplayStats(Item item)
    {
        image.sprite = item.sprite;
        image.preserveAspect = true;
        itemName.color = item.color;
        itemName.text = item.displayName;
        itemDescription.text = item.description;

        string stats = "";

        if (item.type == Item.itemType.weapon)
        {
            Weapon weapon = (Weapon)item;

            if (weapon.manualDPS == 0)
            {
                float DPS = weapon.damage * weapon.attackSpeed;
                stats += DPS.ToString();
            }
            else
            {
                stats+= weapon.manualDPS.ToString();
            }
            itemStats.text = stats + " DPS";
        }
        else
        {
            Armor armor = (Armor)item;

            if (armor.lifeSteal != 0)
            {
                stats += "LifeSteal: " + armor.lifeSteal + " \n";
            }
            if (armor.defense != 0)
            {
                stats += "Defense: " + armor.defense + " \n";
            }
            if (armor.speed != 0)
            {
                stats += "Speed: " + armor.speed + " \n";
            }
            if (armor.health != 0)
            {
                stats += "Health: " + armor.health + " \n";
            }
            if (armor.strength != 0)
            {
                stats += "Strength: " + armor.strength + " \n";
            }
            if (armor.crit != 0)
            {
                stats += "Crit: " + armor.crit + " \n";
            }
            if (armor.numProjectiles != 0)
            {
                stats += "+" + armor.numProjectiles  + " projectiles \n";
            }
            if (armor.projectileSpeed != 0)
            {
                stats += "+" + armor.projectileSpeed + " projectile Speed \n";
            }
            if (armor.attackSpeed != 0)
            {
                stats += "+" + armor.attackSpeed + " attack speed \n";
            }
            if (armor.AOE != 0)
            {
                stats += "+" + armor.AOE + " AOE \n";
            }
            if (armor.range != 0)
            {
                stats += "+" + armor.range + " range \n";
            }
            if (armor.knockBack != 0)
            {
                stats += "+" + armor.knockBack + " knockback \n";
            }
            itemStats.text = stats;
        }
    }

    public void DisplayComparedStats(Item item, Item itemToCompare)
    { 
        image.sprite = item.sprite;
        image.preserveAspect = true;
        itemName.text = item.displayName;
        itemName.color = item.color;
        itemDescription.text = item.description;

        if (item.type == Item.itemType.weapon)
        {
            Weapon weapon = (Weapon)item;
            Weapon weaponToCompare = (Weapon)itemToCompare;
             

            float DPS = 0;
            float compareDPS = 0;

            if (weapon.manualDPS == 0)
            {
                DPS = weapon.damage * weapon.attackSpeed; 
            }
            else
            {
                DPS = weapon.manualDPS; 
            }

            if (weaponToCompare.manualDPS == 0)
            {
                compareDPS = weaponToCompare.damage * weaponToCompare.attackSpeed; 
            }
            else
            {
                compareDPS = weaponToCompare.manualDPS; 
            }

            if (DPS >= compareDPS)
            { 
                itemStats.text = "+"+(DPS - compareDPS).ToString() + " DPS";
            }
            else
            { 
                itemStats.text = (DPS - compareDPS).ToString() + " DPS";
            } 
        }
         
        else
        {
            string stats = "";
            Armor armor = (Armor)item;
            Armor armorToCompare = (Armor)itemToCompare;

            if (armor.lifeSteal != 0 || armorToCompare.lifeSteal!=0)
            {
                if (armor.lifeSteal >= armorToCompare.lifeSteal)
                { 
                    stats += "LifeSteal: +" + (armor.lifeSteal - armorToCompare.lifeSteal).ToString() + " \n";
                }
                else
                { 
                    stats += "LifeSteal: " + (armor.lifeSteal - armorToCompare.lifeSteal).ToString() + " \n";
                } 
            }
            if (armor.defense != 0 || armorToCompare.defense != 0)
            {
                if (armor.defense >= armorToCompare.defense)
                {
                    stats += "Defense: +" + (armor.defense - armorToCompare.defense).ToString() + " \n";
                }
                else
                { 
                    stats += "Defense: " + (armor.defense - armorToCompare.defense).ToString() + " \n";
                } 
            }
            if (armor.speed != 0 || armorToCompare.speed != 0)
            {
                if (armor.speed >= armorToCompare.speed)
                {
                    stats += "Speed: +" + (armor.speed - armorToCompare.speed).ToString() + " \n";
                }
                else
                { 
                    stats += "Speed: " + (armor.speed - armorToCompare.speed).ToString() + " \n";
                } 
            }
            if (armor.health != 0 || armorToCompare.health != 0)
            {
                if (armor.health >= armorToCompare.health)
                {
                    stats += "Health: +" + (armor.health - armorToCompare.health).ToString() + " \n";
                }
                else
                { 
                    stats += "Health: " + (armor.health - armorToCompare.health).ToString() + " \n";
                } 
            }
            if (armor.strength != 0 || armorToCompare.strength != 0)
            {
                if (armor.strength >= armorToCompare.strength)
                {
                    stats += "Strength: +" + (armor.strength - armorToCompare.strength).ToString() + " \n";
                }
                else
                { 
                    stats += "Strength: " + (armor.strength - armorToCompare.strength).ToString() + " \n";
                } 
            }
            if (armor.crit != 0 || armorToCompare.crit != 0)
            {
                if (armor.crit >= armorToCompare.crit)
                {
                    stats += "Crit: +" + (armor.crit - armorToCompare.crit).ToString() + " \n";
                }
                else
                {
                    stats += "Crit: " + (armor.crit - armorToCompare.crit).ToString() + " \n"; 
                } 
            }

            if (armor.numProjectiles != 0 || armorToCompare.numProjectiles!=0)
            {
                if (armor.numProjectiles >= armorToCompare.numProjectiles)
                { 
                    stats += "+" + (armor.numProjectiles - armorToCompare.numProjectiles).ToString() + " projectiles \n";
                }
                else
                { 
                    stats += (armor.numProjectiles - armorToCompare.numProjectiles).ToString() + " projectiles \n";
                } 
            }
            if (armor.projectileSpeed != 0 || armorToCompare.projectileSpeed != 0)
            {
                if (armor.projectileSpeed >= armorToCompare.projectileSpeed)
                {
                    stats += "+" + (armor.projectileSpeed - armorToCompare.projectileSpeed).ToString() + " projectile speed\n";
                }
                else
                { 
                    stats += (armor.projectileSpeed - armorToCompare.projectileSpeed).ToString() + " projectile speed \n";
                } 
            }
            if (armor.range != 0 || armorToCompare.range != 0)
            {
                if (armor.range >= armorToCompare.range)
                {
                    stats += "+" + (armor.range - armorToCompare.range).ToString() + " range \n";
                }
                else
                { 
                    stats += (armor.range - armorToCompare.range).ToString() + " range \n";
                } 
            }
            if (armor.AOE != 0 || armorToCompare.AOE != 0)
            {
                if (armor.AOE >= armorToCompare.AOE)
                {
                    stats += "+" + (armor.AOE - armorToCompare.AOE).ToString() + " AOE \n";
                }
                else
                { 
                    stats += (armor.AOE - armorToCompare.AOE).ToString() + " AOE \n";
                } 
            }
            if (armor.knockBack != 0 || armorToCompare.knockBack != 0)
            {
                if (armor.knockBack >= armorToCompare.knockBack)
                {
                    stats += "+" + (armor.knockBack - armorToCompare.knockBack).ToString() + " knockback \n";
                }
                else
                { 
                    stats += (armor.knockBack - armorToCompare.knockBack).ToString() + " knockback\n";
                } 
            }
            if (armor.attackSpeed != 0 || armorToCompare.attackSpeed != 0)
            {
                if (armor.attackSpeed >= armorToCompare.attackSpeed)
                {
                    stats += "+" + (armor.attackSpeed - armorToCompare.attackSpeed).ToString() + "attack speed\n";
                }
                else
                {
                    stats += (armor.attackSpeed - armorToCompare.attackSpeed).ToString() + " attack speed \n"; 
                } 
            }
            itemStats.text = stats;
        }
    }
}
