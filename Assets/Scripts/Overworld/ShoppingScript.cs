using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Made by Gary Kupinski and Jonas Clermont

public class ShoppingScript : MonoBehaviour
{
    [SerializeField]
    int money;
    public Text itemsList;
    public Text description;
    public Text moneyText;
    public Text shopNameText;
    public ShopDB inventory;
    public GameObject shopMenuCanvas;
    int currentSelection = 0;
    float delay = 0f;
    float warningTimer = 0f;

    void Start() {
        UpdateList(0); //Sets Item list to start at 0
        UpdateDescription(0);
        UpdateMoney(); //Checks money after and during purchase
        shopNameText.text = inventory.shopName;
       
    }

    void Update() {
        float rawInput = Input.GetAxisRaw("Vertical");
        if (delay <= 0f) { //Moving the visual selection arrow
            if (rawInput > 0.2f) {
                currentSelection--;
                delay = 0.2f;
            } else if (rawInput < -0.2f) {
                currentSelection++;
                delay = 0.2f;
            }
            currentSelection %= inventory.inventory.Length;
            if (currentSelection < 0) {
                currentSelection += inventory.inventory.Length;
            }
            UpdateList(currentSelection);
            UpdateDescription(currentSelection);
        } else if (Mathf.Abs(rawInput) < 0.2f) {
            delay = 0;
        }
        delay -= Time.deltaTime;
        ShopTestItem currentItem = inventory.inventory[currentSelection];
        if (Input.GetButtonDown("Submit")) { //Checks money for item
            if (money >= currentItem.price) {
                money -= currentItem.price;
                //TODO: Give item to the player
            } else {
                warningTimer = 3f;
                description.text = "Not enough money to buy this item!"; //Changes Description to Warning
            }
        }
        UpdateMoney();
        warningTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Cancel")) { //Exiting Shop
            IsShopping.shopping = false;
            shopMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    void UpdateList(int i) {
        string tempInvList = "";
        for (int x = 0; x < inventory.inventory.Length; x++) {
            ShopTestItem item = inventory.inventory[x];
            if (x == i) {
                tempInvList += "> ";
            }
            tempInvList += item.name + ": $" + item.price + "\n\n\n";
        }
        itemsList.text = tempInvList;
    }

    void UpdateDescription(int i) { //Changes description for item
        if (warningTimer > 0f) {
            return;
        }
        ShopTestItem item = inventory.inventory[i];
        description.text = item.name + "\n" + item.description;
    }
    void UpdateMoney() {
        moneyText.text = "Money: " + money;
    }

}
