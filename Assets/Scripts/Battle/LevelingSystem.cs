using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LevelingSystem : MonoBehaviour
{
    //Created by Mike Lecus  | Date: 11/8 - 11/11

    public Text descriptionText;
    public int xpPoints = 9999999;

    [SerializeField] private int health;        //Stats will change when able to implement them with a ScriptableObject for PlayerStats
    [SerializeField] private int amplitude;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private float accuracy;
    [SerializeField] private int flute;
    [SerializeField] private int guitar;
    [SerializeField] private int piano;

    public Text xpDisplay;                      //All the stats' text fields
    public Text healthValue;
    public Text amplitudeValue;
    public Text attackValue;
    public Text defenseValue;
    public Text accuracyValue;

    public Text fluteValue;                     //All the weapon's text fields
    public Text guitarValue;
    public Text pianoValue;

    public Inventory inventory;
    public PlayerStatsSO player;

    public GameObject[] weapon;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventory.weapons.Length; i++)
        {
            weapon[i].gameObject.SetActive(true);
            weapon[i].gameObject.name = i.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        xpDisplay.text = "Experience:		" + xpPoints.ToString();                                   //This makes the text fields hold the appropriate stat with the value it has
        healthValue.text = "Health		" + health.ToString();
        amplitudeValue.text = "Amplitude		" + amplitude.ToString();
        attackValue.text = "Attack		" + attack.ToString();
        defenseValue.text = "Defense		" + defense.ToString();
        accuracyValue.text = "Accuracy		" + accuracy.ToString() + "%";

        fluteValue.text = flute.ToString();
        guitarValue.text = guitar.ToString();
        pianoValue.text = piano.ToString();

        accuracy = Mathf.Round(accuracy * 100f) / 100;
    }

    public void HealthDisplay()                                                         //These methods are for clicking on a stat and getting a description of it in the description box
    {
        descriptionText.text = "The amount of the player's health. Increases by 5.";
    }

    public void AmplitudeDisplay()
    {
        descriptionText.text = "The amount of power for player attacks. Increases by 2.";
    }

    public void AttackDisplay()
    {
        descriptionText.text = "The amount of damage the player does when attacking. Increases by 10";
    }

    public void DefenseDisplay()
    {
        descriptionText.text = "Decreases the amount of damage the player receives when the enemy attacks. Increases by 1.";
    }

    public void AccuracyDisplay()
    {
        descriptionText.text = "Increases the window of time where an action is considered on-beat. Increases by 0.2.";
    }

    public void FluteDisplay()
    {
        descriptionText.text = "A woodwind insturment.";
    }
    public void GuitarDisplay()
    {
        descriptionText.text = "A string insturment.";
    }
    public void PianoDisplay()
    {
        descriptionText.text = "A keyboard insturment.";
    }


    public void HealthLevelUp()                                                     //For the buttons to level up the appropriate stat or weapon (instrument)
    {
        if (xpPoints > 0) {
            health += 5;
            xpPoints -= 1;
        }
 
    }

    public void AmplitudeLevelUp()
    {
        if (xpPoints > 0) {
            amplitude += 2;
            xpPoints -= 1;
        }
    }

    public void AttackLevelUp()
    {
        if (xpPoints > 0) {
            attack += 10;
            xpPoints -= 1;
        }
    }

    public void DefenseLevelUp()
    {
        if (xpPoints > 0) {
            defense += 1;
            xpPoints -= 1;
        }
    }

    public void AccuracyLevelUp()
    {
        if (xpPoints > 0) {
            accuracy += 00.2f;
            xpPoints -= 1;
        }
    }

    public void FluteLevelUp()
    {
        if (xpPoints > 0)
        {
            flute += 10;
            xpPoints -= 1;
        }
    }
    public void GuitarLevelUp()
    {
        if (xpPoints > 0)
        {
            guitar += 10;
            xpPoints -= 1;
        }
    }
    public void PianoLevelUp()
    {
        if (xpPoints > 0)
        {
            piano += 10;
            xpPoints -= 1;
        }
    }
}
