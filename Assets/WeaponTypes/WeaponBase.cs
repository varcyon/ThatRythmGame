using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Andy Xiong

    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Create new weapon")]
public class WeaponBase : ScriptableObject{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite inventorySprite;

    [SerializeField] WeaponType type1;

    [SerializeField] int weaponDamage;
}

public enum WeaponType{
    None,
    Woodwinds,
    Brass,
    Electronic,
    String,
    Keyboard,
    Percussion
}
//For the Type chart it's the first letters of the weapons and the multiplier that the weapon gets. 
public class TypeChart{
    static float[][] chart ={
       //                   W   B   E   S   K   P
       /*W*/ new float[] { 1f, 0.5f, 1.5f, 1f, 1f, 1f },
       /*B*/ new float[] { 1.5f, 1f, 0.5f, 1.5f, 1f, 0.5f },
       /*E*/ new float[] { 0.5f, 1.5f, 1f, 1f, 0.5f, 0.5f },
       /*S*/ new float[] { 1f, 0.5f, 1f, 1f, 1.5f, 1f },
       /*K*/ new float[] { 1f, 1f, 1.5f, 0.5f, 1f, 1.5f },
       /*P*/ new float[] { 1f, 1.5f, 1.5f, 1f, 0.5f, 1f }
    };

    public static float GetEffectiveness(WeaponType attackType, WeaponType defenseType){
        if (attackType == WeaponType.None || defenseType == WeaponType.None)
            return 1;

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }
}