using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Made by Gary Kupinski and Jonas Clermont
[CreateAssetMenu(fileName = "ShopDB", menuName = "ScriptableObjects/ShopDB", order = 1)]
public class ShopDB : ScriptableObject
{
    public string shopName; //Sets shop name in DB
    public string contextScene; //Sets what scene it will take you back to
    public ShopTestItem[] inventory;
}
