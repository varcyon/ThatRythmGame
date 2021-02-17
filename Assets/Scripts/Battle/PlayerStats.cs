using UnityEngine;
using UnityEngine.UI;
//Script made by Dan Urbanczyk
public class PlayerStats : MonoBehaviour{
    #region VARIABLES
    private float playerMaxHealth = 1000; // This health stat needs to be private or else the slider will not react to changes to the variable.
    public float playerCurrentHealth = 1000;
    [HideInInspector]public float maxHealthReference; // This is used to reference health inrease outside of this script.

    [SerializeField] public Slider sliderHealth; // Gives script access to the Health slider
    [SerializeField] public Text healthText; // This represents the number inside the Health bar

    [HideInInspector]public float amplitude = 1.00f;
    [SerializeField] public Slider sliderAmp;
    [SerializeField] public Text ampText;

    public float accuracy = 1.05f;
    public float efficiency = .05f;
    public int playerBaseDamage = 50;
    public int playerBaseDefense = 25;
    
    //These will be used to signify the damage output of the player and equipped weapon based on amplitude, weapon damage, and base damage. 
    //Most liikely these will just be referenced form another script.
    public float WeaponOneAttack;
    public float WeaponTwoAttack;
    public float WeaponThreeAttack;
    //[HideInInspector]public int playerLevel; Commented because we will need it but it might not be in this script
    private void Start() {
        playerMaxHealth = playerMaxHealth + maxHealthReference;
    }

    #endregion
    void Update(){
        playerCurrentHealth = Mathf.Clamp(playerCurrentHealth, 0f, playerMaxHealth);
        sliderHealth.maxValue = playerMaxHealth;
        sliderHealth.minValue = 0;
        amplitude = Mathf.Clamp(amplitude, 0f, 1.0f);

        //These weaepon Attack values may want to be added to a attack method instead of constantly being checked in an update
        WeaponOneAttack = (playerBaseDamage /* + weapon 1 damage from what will be weapon stats or inventory script*/) * amplitude;
        WeaponTwoAttack = (playerBaseDamage /* + weapon 2 damage*/) * amplitude;
        WeaponThreeAttack = (playerBaseDamage /* + weapon 3 damage*/) * amplitude;
        WeaponOneAttack = Mathf.RoundToInt(WeaponOneAttack);
        WeaponTwoAttack = Mathf.RoundToInt(WeaponTwoAttack);
        WeaponThreeAttack = Mathf.RoundToInt(WeaponThreeAttack);
        //===========================================================
        playerCurrentHealth = Mathf.RoundToInt(playerCurrentHealth);
        sliderHealth.value = playerCurrentHealth;
        healthText.text = "" + playerCurrentHealth.ToString("0") + "/" + playerMaxHealth.ToString("0");
        sliderAmp.value = amplitude;
        float amplitudePercentage = amplitude * 100;
        ampText.text = amplitudePercentage.ToString("0.0") + "%";
        /*if (Input.GetKeyDown(KeyCode.A)) {
            playerCurrentHealth--;
            amplitude -= .02f;
            Debug.Log(amplitude);
            Debug.Log(playerCurrentHealth);
            Debug.Log(WeaponOneAttack);
        }
        if (Input.GetKeyDown(KeyCode.S)) {          //This commented section was used for testing variables with player input.
            Debug.Log(WeaponTwoAttack);
            Debug.Log(WeaponThreeAttack);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            amplitude += .04f;
            playerCurrentHealth += 10;
        }*/
    }
}
