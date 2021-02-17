using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public Text itemName;
    public Text price;
    public ShopTestItem itemToBuy;

    public ShopUI shopUI;

    public void PurchaseItem()
    {
        shopUI.Purchase(itemToBuy);
    }    

}
