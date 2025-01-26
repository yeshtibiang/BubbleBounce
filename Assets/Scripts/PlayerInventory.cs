using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<KeySO> collectedKeys = new List<KeySO>();

    public void CollectKey(KeySO key)
    {
        if (!collectedKeys.Contains(key))
        {
            collectedKeys.Add(key);
        }
    }

    public bool HasKey(KeySO key)
    {
        return collectedKeys.Contains(key);
    }

    public void RemoveKey(KeySO key)
    {
        if (collectedKeys.Contains(key))
        {
            collectedKeys.Remove(key);
        }
    }
}
