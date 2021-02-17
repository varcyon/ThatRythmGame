using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public Transform lastPosition;
    public float maxHealth;
    public float currentHealth;
    public float amplitude = 1.0f;
    public float accuracy = 1.05f;
    public float efficiency = .05f;
    public int baseDamage = 50;
    public int baseDefense = 25;
    public WeaponBase[] equippedWeapons = new WeaponBase[3];
}
