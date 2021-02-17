using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemEffect {
    MinorHeal,
    MediumHeal,
    MajorHeal,
    MinorAccuracy,
    MediumAccuracy,
    MajorAccuracy,
    SlowTime
}
[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Inventory/New Item")]
public class ItemSO : ScriptableObject
{    
    public string itemName;
    public ItemEffect effect;
    public int itemCount;
}
