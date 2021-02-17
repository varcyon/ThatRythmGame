using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    // Created by Mike Lecus, Hanul Huh, and Jonas Clermont

    public ShopDB shop;

    public Text moneyText;
    public Text descriptionText;
    public RectTransform itemListArea;
    public GameObject shopItem;
    public GameObject ShopMenuUI;

    public float money;


    // Start is called before the first frame update
    void Start()
    {
        moneyText.text = "Money: " + money;
        /*foreach(var a in shop.inventory)
        {
            Instantiate(shopItem, itemListArea);
            shopItem.transform.parent = itemListArea;
        }*/

        for (int i = 0; i < shop.inventory.Length; i++)
        {
            GameObject currentItem = Instantiate(shopItem, itemListArea);
            RectTransform itemPos = currentItem.GetComponent<RectTransform>();
            itemPos.anchorMin = new Vector2(0, 1 - 0.2f * i - 0.2f);
            itemPos.anchorMax = new Vector2(1, 1 - 0.2f * i);
            ShopItem item = currentItem.GetComponent<ShopItem>();
            item.itemName.text = shop.inventory[i].name;
            Text _itemName = item.itemName;
            item.price.text = shop.inventory[i].price.ToString();
            Text itemPrice = item.price;

            item.itemToBuy = shop.inventory[i];
            item.shopUI = this;

            Debug.Log(shop.inventory[i].name);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        { //Exiting Shop
            IsShopping.shopping = false;
            ShopMenuUI.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Purchase(ShopTestItem currentItem)
    {
        if (money >= currentItem.price)
        {
            money -= currentItem.price;
            moneyText.text = "Money: " + money;
            //TODO: Give item to the player
        }
        /*else
        {
            warningTimer = 3f;
            description.text = "Not enough money to buy this item!"; //Changes Description to Warning
        }*/
    }

    public void Escape()
    {
        IsShopping.shopping = false;
        ShopMenuUI.SetActive(false);
        Time.timeScale = 1f;

    }
}




