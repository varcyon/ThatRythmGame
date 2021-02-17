using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Script Made by Gary Kupinski

public class HealthBar : MonoBehaviour {

    #region VARIABLES   
    public PlayerStatsSO playerStats;
    [SerializeField] public Slider sliderHealth; // Gives script access to the Health slider
    [SerializeField] public Text healthText; // This represents the number inside the Health bar
    #endregion
    void Update() {
        playerStats.currentHealth = Mathf.Clamp(playerStats.currentHealth, 0f, playerStats.maxHealth);
        sliderHealth.maxValue = playerStats.maxHealth;
        sliderHealth.minValue = 0;
        
        //PlayerPrefs.SetFloat("currentHealth", 500f); Testing PlayerPrefs

        sliderHealth.value = Mathf.RoundToInt(playerStats.currentHealth);
        healthText.text = "" + Mathf.RoundToInt(playerStats.currentHealth).ToString("0") + "/" + playerStats.maxHealth.ToString("0");
    }
}
