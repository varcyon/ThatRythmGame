using Ludiq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour {
    public Inventory inventory;
    public PlayerStatsSO player;
    public Text itemDescription;
    public Text errorText;
    #region itemCounts
    public Text minorHp;
    public Text mediumHp;
    public Text majorHp;
    public Text minorAcc;
    public Text mediumAcc;
    public Text majorAcc;
    public Text mgclMtrnm;
    #endregion

    private void Start() {       
        UpdateCounts();
    }
    public void ItemEffects(ItemEffect itemEffect) {
        UpdateCounts();
        switch (itemEffect) {
            case ItemEffect.MinorHeal:
                Debug.Log(itemEffect);
                //player.currentHealth += 25; //Placeholder value, can change to percentage value for beter scaling as well
                break;
            case ItemEffect.MediumHeal:
                Debug.Log(itemEffect);
                //player.currentHealth += 60; //Placeholder value, can change to percentage value for beter scaling as well
                break;
            case ItemEffect.MajorHeal:
                Debug.Log(itemEffect);
                //player.currentHealth += 100; //Placeholder value, can change to percentage value for beter scaling as well
                break;
            case ItemEffect.MinorAccuracy: //Duration not yet implemented
                Debug.Log(itemEffect);
                // player.accuracy += .1f;
                break;
            case ItemEffect.MediumAccuracy: //Duration not yet implemented
                Debug.Log(itemEffect);
                //player.accuracy += .2f;
                break;
            case ItemEffect.MajorAccuracy: //Duration not yet implemented
                Debug.Log(itemEffect);
                // player.accuracy += .3f;
                break;
            case ItemEffect.SlowTime:   //May be changed to affect pitch instead of timeScale
                Debug.Log(itemEffect);
                //Time.timeScale = .7f;
                break;
        }
    }
    public void UseItem(int itemID) {
        if(inventory.inventory[itemID].itemCount > 0) {
            inventory.inventory[itemID].itemCount -= 1;
            ItemEffects(inventory.inventory[itemID].effect);            
        } else {
            itemDescription.text = null;
            errorText.text = "I don't have any " + inventory.inventory[itemID].itemName + "s.";
        }
        
    }
    void UpdateCounts() {
        minorHp.text = "" + inventory.inventory[4].itemCount;      //This is currently super janky and the array index depends on the _inventory
        mediumHp.text = "" + inventory.inventory[2].itemCount;     //
        majorHp.text = "" + inventory.inventory[0].itemCount;      //
        minorAcc.text = "" + inventory.inventory[5].itemCount;     //Keeps track of how many items of each type are available.
        mediumAcc.text = "" + inventory.inventory[3].itemCount;    //
        majorAcc.text = "" + inventory.inventory[1].itemCount;     //
        mgclMtrnm.text = "" + inventory.inventory[6].itemCount;    //
    }
}  
