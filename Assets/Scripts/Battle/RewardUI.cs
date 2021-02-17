using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Andy Xiong
public class RewardUI : MonoBehaviour
{
    public int currentZone = 1; //The int would be the current stage level zone
    private float rewardModifier = 1.1f; //Set Modifier

    public int baseMoney = 10; //Just a test variable can be swithced
    public int baseExperience = 10; //Just a test variable can be switched
    private float rewardMoney;
    private float rewardExperience;
    private float maxMoney;

    public Text RewardMT; //Money Text
    public Text RewardET; //Experience Text
    public Text itemText;
    //public Image itemSprite;

    // Start is called before the first frame update
    void Start() {
        if (SceneManager.GetActiveScene().name == "RewardUI") {
            Money();
            Experience();
            Item();
        }
        else{
            //nothing
        }
    }
    
    public void Item(){
        float rewardItem = Random.value;
        if (rewardItem < .45f){
            itemText.text = "Item Has Dropped";
        }
        else{
            itemText.text = "No Item Has Dropped";
        }
    }

    public void Money(){
        rewardMoney = baseMoney * currentZone * rewardModifier;
        maxMoney = maxMoney + rewardMoney;
        RewardMT.text = "+ " + rewardMoney + "   /   " + maxMoney;
    }

    public void Experience(){
        rewardExperience = baseExperience * currentZone * rewardModifier;
        RewardET.text = "+ " + rewardExperience;
    }

    private void Update(){
        if (Input.GetMouseButton(0)){ //Input.anyKey
            SceneManager.LoadScene("Main");
        }
    }
}
