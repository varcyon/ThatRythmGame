using UnityEngine;
using UnityEngine.UI;
//Script made by Dan Urbanczyk
public class PlayerManager : MonoBehaviour{
    #region VARIABLES
    public PlayerStatsSO playerStatsSO;

    [SerializeField] public Slider sliderHealth; // Gives script access to the Health slider
    [SerializeField] public Text healthText; // This represents the number inside the Health bar

    [SerializeField] public Slider sliderAmp;
    [SerializeField] public Text ampText;

    //These will be used to signify the damage output of the player and equipped weapon based on amplitude, weapon damage, and base damage. 
    //Most liikely these will just be referenced form another script.
    public float WeaponOneAttack;
    public float WeaponTwoAttack;
    public float WeaponThreeAttack;
    //[HideInInspector]public int playerLevel; Commented because we will need it but it might not be in this script
    private void Start() {
        WeaponOneAttack = playerStatsSO.baseDamage;
        WeaponTwoAttack = playerStatsSO.baseDamage;
        WeaponThreeAttack = playerStatsSO.baseDamage;
    }

    #endregion
    void Update(){
        playerStatsSO.currentHealth = Mathf.Clamp(playerStatsSO.currentHealth, 0f, playerStatsSO.maxHealth);
        sliderHealth.maxValue = playerStatsSO.maxHealth;
        sliderHealth.minValue = 0;
        playerStatsSO.amplitude = Mathf.Clamp(playerStatsSO.amplitude, 0f, 1.0f);

        //These weaepon Attack values may want to be added to a attack method instead of constantly being checked in an update
        WeaponOneAttack = (playerStatsSO.baseDamage /* + weapon 1 damage from what will be weapon stats or inventory script*/) * playerStatsSO.amplitude;
        WeaponTwoAttack = (playerStatsSO.baseDamage /* + weapon 2 damage*/) * playerStatsSO.amplitude;
        WeaponThreeAttack = (playerStatsSO.baseDamage /* + weapon 3 damage*/) * playerStatsSO.amplitude;
        WeaponOneAttack = Mathf.RoundToInt(WeaponOneAttack);
        WeaponTwoAttack = Mathf.RoundToInt(WeaponTwoAttack);
        WeaponThreeAttack = Mathf.RoundToInt(WeaponThreeAttack);
        //===========================================================
        playerStatsSO.currentHealth = Mathf.RoundToInt(playerStatsSO.currentHealth);
        sliderHealth.value = playerStatsSO.currentHealth;
        healthText.text = "" + playerStatsSO.currentHealth.ToString("0") + "/" + playerStatsSO.maxHealth.ToString("0");
        sliderAmp.value = playerStatsSO.amplitude;
        float amplitudePercentage = playerStatsSO.amplitude * 100;
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
