using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Player Inventory", menuName = "ScriptableObjects/Inventory/PlayerInventory")]
public class Inventory : ScriptableObject {
    public int inventoryLimit;
    public ItemSO[] inventory;
    public WeaponBase[] weapons;
}
