using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RewardScreenDisplay : MonoBehaviour
{
    //money, xp, dmg
    private int money = 0;
    public Text moneyText;

    private int exp = 0;
    public Text expText;

    private int damage = 100;
    public Text damageText;

    void Update()
    {
        moneyText.text = "Money Earned : " + money;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            money++;
        }

        expText.text = "Experience Earned : " + exp;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            exp = 1 + 1;
        }

        damageText.text = "Damage Dealt : " + damage;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            damage = damage + 50;
        }
    }

    public void continueButton()
    {
        SceneManager.LoadScene("Overworld Test Area");
    }
}
