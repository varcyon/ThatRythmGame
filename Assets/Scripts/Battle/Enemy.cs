using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    public EnemyStatsSO enemyStatSO;
    public Slider enemyHealthBar; //Attach EnemyHealthBar prefab here
    
    void Start() {
        Debug.Log("Set enemy health");
        enemyHealthBar.maxValue = enemyStatSO.enemyMaxHealth;
        enemyHealthBar.value = enemyStatSO.enemyHealth;
    }
    public void Update() {
        enemyHealthBar.value = enemyStatSO.enemyHealth;
    }

    /*public void TestButton() {
        enemyStatSO.enemyHealth -= 10; //Used to test health bar display
    }*/
}
