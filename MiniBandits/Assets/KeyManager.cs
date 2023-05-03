using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    int keys;

    public void AddKeys(int goldToAdd)
    {
        keys += goldToAdd;
    }
    public void SpendKeys(int goldToSpend)
    {
        keys -= goldToSpend;
    }
    public int GetKeys()
    {
        return keys;
    }
}
