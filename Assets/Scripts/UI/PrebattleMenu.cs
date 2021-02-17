using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Security.Policy;
using Ludiq;
using System.ComponentModel;
using System;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;
using UnityEditor.ShaderGraph.Internal;
using TMPro;

public class PrebattleMenu : MonoBehaviour
{
    //ScriptableObject Data
    public EnemyStatsSO enemy;
    public Inventory inventory;
    public PlayerStatsSO player;    

    public WeaponBase[] equippedWeapons = new WeaponBase[3];
    public int numOfWeapons = 0;
    public bool[] isEquipped = new bool[6];
    public Vector3[] unequipSlot = new Vector3[3];

    float timeMod = 1.0f;
    float accMod = 0f;    

    #region UI Elements
    //Weapon Selection
    [SerializeField]
    public Button[] weapons;

    //Start/Run
    public Button startBattle;
    public Button escapeBattle;

    //Consumables
    public Toggle minorAccPot;
    public Toggle mediumAccPot;
    public Toggle majorAccPot;
    public Toggle timePot;
    
    //Weapon Display
    [SerializeField]
    public Transform[] equipSlot;

    public Text enemyStats;
    public Text playerStats;
    public Text minorHp;
    public Text mediumHp;
    public Text majorHp;
    public Text minorAcc;
    public Text mediumAcc;
    public Text majorAcc;
    public Text timeSlow;
    #endregion

    private void Start() {
        player.accuracy = 1;
        for(int i = 0; i < 6; i++) {
            isEquipped[i] = false;
        }
        for(int i = 0; i < inventory.weapons.Length; i++) {
            weapons[i].gameObject.SetActive(true);
            weapons[i].gameObject.name = i.ToString();
        }
        UpdateUI();
    }
    void UpdateUI() {   //Updates UI to reflect new stat values
        playerStats.text = "HP: " + "\n\nAccuracy Modifier: " + (player.accuracy + accMod) + "\n\nTime Modifier: " + timeMod + "\n\nBase Damage: " + player.baseDamage + "\t\tBase Defense: " + player.baseDefense;
        enemyStats.text = enemy.enemyType + "\t\t\t" + enemy.enemyType2;
        minorHp.text = "" + inventory.inventory[4].itemCount;
        mediumHp.text = "" + inventory.inventory[2].itemCount;
        majorHp.text = "" + inventory.inventory[0].itemCount;
        minorAcc.text = "" + inventory.inventory[5].itemCount;
        mediumAcc.text = "" + inventory.inventory[3].itemCount;
        majorAcc.text = "" + inventory.inventory[1].itemCount;
        timeSlow.text = "Time Slow (" + inventory.inventory[6].itemCount + ")";
    }
    public void UseItem(int itemID) {        
        if (inventory.inventory[itemID].itemCount > 0) {            
            ItemEffects(inventory.inventory[itemID].effect, itemID);
        } else {
            //itemDescription.text = null;
            //errorText.text = "I don't have any " + inventory.inventory[itemID].itemName + "s.";
        }
    }
    public void ItemEffects(ItemEffect itemEffect, int itemID) {
        switch (itemEffect) {
            case ItemEffect.MinorHeal:
                if(player.currentHealth < player.maxHealth) {
                    Debug.Log(itemEffect);
                    player.currentHealth += 25; //Placeholder value, can change to percentage value for beter scaling as well
                    inventory.inventory[itemID].itemCount -= 1;
                }                
                break;
            case ItemEffect.MediumHeal:
                if(player.currentHealth < player.maxHealth) {
                    Debug.Log(itemEffect);
                    player.currentHealth += 60; //Placeholder value, can change to percentage value for beter scaling as well
                    inventory.inventory[itemID].itemCount -= 1;
                }                
                break;
            case ItemEffect.MajorHeal:
                if (player.currentHealth < player.maxHealth) {
                    Debug.Log(itemEffect);
                    player.currentHealth += 100; //Placeholder value, can change to percentage value for beter scaling as well
                    inventory.inventory[itemID].itemCount -= 1;
                }                
                break;
            case ItemEffect.MinorAccuracy: //Duration not yet implemented                
                if (minorAccPot.isOn) {
                    accMod = .1f;
                } else {
                    accMod = 0f;
                }
                Debug.Log(itemEffect);                
                break;
            case ItemEffect.MediumAccuracy: //Duration not yet implemented
                if (mediumAccPot.isOn) {
                    accMod = .2f;
                } else {
                    accMod = 0f;
                }                
                Debug.Log(itemEffect);               
                break;
            case ItemEffect.MajorAccuracy: //Duration not yet implemented                
                if (majorAccPot.isOn) {
                    accMod = .3f;
                } else {
                    accMod = 0f;
                }               
                Debug.Log(itemEffect);                
                break;
            case ItemEffect.SlowTime:   //May be changed to affect pitch instead of timeScale
                if (timePot) {
                    timeMod = .3f;
                } else {
                    timeMod = 0f;
                }
                Debug.Log(itemEffect);
                //Time.timeScale = .7f;
                break;
        }        
        UpdateUI();
    }
    public void EquipButton() {
        EquipWeapon(int.Parse(EventSystem.current.currentSelectedGameObject.name.ToString()));  //Equips corresponding weapon
    }
    public void EquipWeapon(int weaponID) {
        if (!isEquipped[weaponID] && numOfWeapons < 3) {                                        //Checks for room to equip the weapon
            isEquipped[weaponID] = true;                                                        //Sets the weapon as equipped
            equippedWeapons[numOfWeapons] = inventory.weapons[weaponID];                        //Equips the weapon
            unequipSlot[numOfWeapons] = weapons[weaponID].transform.localPosition;              //Saves the weapons inventory slot
            weapons[weaponID].transform.position = equipSlot[numOfWeapons].transform.position;  //Moves the weapon to the correct slot            
            numOfWeapons += 1;                                                                  //Increments the number of equipped weapons
        } else if (isEquipped[weaponID]) {                                                      //Unequips weapon if it's already equipped
            for(int i = 0; i < equippedWeapons.Length; i++) {
                if (inventory.weapons[weaponID] == equippedWeapons[i]) {                        //Checks if weapon is already equipped
                    weapons[weaponID].transform.localPosition = unequipSlot[i];                 //Returns the weapon to its inventory slot        
                    isEquipped[weaponID] = false;                                               //Marks the weapon unequipped
                    equippedWeapons[i] = null;                                                  //Unequips the weapon
                    numOfWeapons -= 1;                                                          //Decrements the number of equipped weapons                                        
                    for (int j = 0; j < equippedWeapons.Length - 1; j++) {
                        if (equippedWeapons[j] == null && j < 2) {                              //Determines if a weapon is equipped in the slot
                            equippedWeapons[j] = equippedWeapons[j + 1];                        //If the slot is empty, the next weapon is moved into it
                            unequipSlot[j] = unequipSlot[j + 1];                                //Assigns the new unequip slot to the new weapon in the equipped slot
                            equippedWeapons[j + 1] = null;                                      //Empties the equip slot that was just moved
                            for(int k = 0; k < inventory.weapons.Length; k++) {
                                if (equippedWeapons[j] == inventory.weapons[k]) {
                                    weapons[k].transform.position = equipSlot[j].transform.position; //Assigns the correct transform position to each of the remaining equipped weapons
                                }
                            }                            
                        }
                    }
                    break;
                } else {
                    isEquipped[weaponID] = false;                                               //Sets weapon to unequipped if not actually equipped
                }                              
            }            
        }
    }
    public void Escape() {                                      //Does not save or load player position, functionality should be added
        SceneManager.LoadScene("Overworld Test Area");
    }
    public void StartBattle() {       
        player.accuracy += accMod;
        if (accMod == .1f) {
            inventory.inventory[5].itemCount -= 1;
        } else if (accMod == .2f) {
            inventory.inventory[3].itemCount -= 1;
        } else if(accMod == .3f) {
            inventory.inventory[1].itemCount -= 1;
        }
        //Time.timeScale -= timeMod;        May be adjusted to pitch mod instead of Time.timeScale
        for (int i = 0; i < equippedWeapons.Length; i++) {               //
            for(int j = 0; j < inventory.weapons.Length; j++) {         //Equips selected weapons for the battle scene
                if(inventory.weapons[j] == equippedWeapons[i]){         //
                    player.equippedWeapons[i] = inventory.weapons[j];   //
                }                                                       
            }                                                           
        }                                                         
        SceneManager.LoadScene("BattleTestScene");  //Loads the battle scene with selected weapons and buffs
    }
}
