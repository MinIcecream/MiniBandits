using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    int gold;

    public void AddGold(int goldToAdd)
    {
        gold += goldToAdd;
    }
    public void SpendGold(int goldToSpend)
    {
        gold -= goldToSpend;
    }
    public int GetGold()
    {
        return gold;
    }
}
