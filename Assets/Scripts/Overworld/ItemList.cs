using Bolt;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemList : MonoBehaviour
{
    
    public PlayerStatsSO player;
    
    public void ItemEffect(string usedItem) {        //Values will need to be assigned to each item effect. 
        if (usedItem == "Minor Health Potion") {
            player.currentHealth += 25;
            Debug.Log(usedItem);
        } else if( usedItem == "Medium Health Potion") {
            player.currentHealth += 50;
            Debug.Log(usedItem);
        } else if(usedItem == "Major Health Potion") {
            player.currentHealth += 100;
            Debug.Log(usedItem);
        } else if(usedItem == "Minor Accuracy Potion") {
            //Increase accuracy by designated amount for a limited time   
            Debug.Log(usedItem);
        } else if(usedItem == "Medium Accuracy Potion") {
            //Increase accuracy by designated amount for a limited time  
            Debug.Log(usedItem);
        } else if(usedItem == "Major Accuracy Potion") {
            //Increase accuracy by designated amount for a limited time
            Debug.Log(usedItem);
        } else if(usedItem == "Magical Metronome") {
            //Slows down the song
            Debug.Log(usedItem);
        } else {
            Debug.Log("Item not found.");
        }   
    }
}
