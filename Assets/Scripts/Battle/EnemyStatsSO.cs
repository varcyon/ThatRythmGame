using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class EnemyStatsSO : ScriptableObject
{
    public float enemyMaxHealth;
    public float enemyHealth;
    public float enemyDmg;
    public float enemyDefense;

    public WeaponType enemyType;
    public WeaponType enemyType2;
 
}
