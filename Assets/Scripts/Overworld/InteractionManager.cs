// This script was made by Dan Urbanczyk
// Added onto by Gary Kupinski
using UnityEngine;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour{
    public Text interactionPopUp; // this is a simple text object that is on the overworld canvas
    public Text npcdialogue;// this is text to test dialogue
    public GameObject shopMenu; // Attach the shop menu to this
    private bool menuCanOpen;
    bool _interacting;
    private string npcType; //the reason for including this is so when we add other NPC's we can add additional checks
    private void Start() {
        interactionPopUp.gameObject.SetActive(false);
        npcdialogue.gameObject.SetActive(false);
        shopMenu.SetActive(false);
    }
    private void Update() {
        if(menuCanOpen == true) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                if(npcType == "shopkeeper") {
                    Time.timeScale = 0f; // pauses the overworld
                    IsShopping.shopping = true;
                    shopMenu.SetActive(true); // shop menu gets opened
                    interactionPopUp.gameObject.SetActive(false);
                }else if(npcType == "NPC" && !_interacting) {
                    _interacting = true;
                    Time.timeScale = 0f; // pauses the overworld
                    interactionPopUp.gameObject.SetActive(false);
                    npcdialogue.gameObject.SetActive(true);
                }else if (_interacting) {
                    npcdialogue.gameObject.SetActive(false);
                    Time.timeScale = 1f; // unpauses the overworld
                    _interacting = false;
                }
            }
            /*if (Input.GetKeyDown(KeyCode.Space) && npcType == "shopkeeper") {
                Time.timeScale = 0f; // pauses the overworld
                IsShopping.shopping = true;
                shopMenu.SetActive(true); // shop menu gets opened
                interactionPopUp.gameObject.SetActive(false);
            }
            if(shopMenu.activeInHierarchy == true) {
                interactionPopUp.gameObject.SetActive(false);
            }
            else {
                interactionPopUp.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Space) && npcType == "NPC") {
                Time.timeScale = 0f; // pauses the overworld
                interactionPopUp.gameObject.SetActive(false);
                npcdialogue.gameObject.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.RightAlt) && npcdialogue.gameObject == true) {
                npcdialogue.gameObject.SetActive(false);
                Time.timeScale = 1f; // unpauses the overworld
            }*/
        }
        else {
            interactionPopUp.gameObject.SetActive(false);
            npcdialogue.gameObject.SetActive(false);
            
        }
    }
    private void OnTriggerEnter(Collider other) { // this is used to check if NPC is near the player, and what type of npc is around.
        if(other.CompareTag("shopkeeper")) {
            interactionPopUp.text = "Press SPACE to Shop";
            interactionPopUp.gameObject.SetActive(true);
            menuCanOpen = true;
            npcType = "shopkeeper";
        }
        if(other.CompareTag("NPC")) {
            interactionPopUp.text = "Press SPACE Talk";
            interactionPopUp.gameObject.SetActive(true);
            menuCanOpen = true;
            npcType = "NPC";
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("shopkeeper")) {
            menuCanOpen = false;
            interactionPopUp.gameObject.SetActive(false);
        }
        if (other.CompareTag("NPC")) {
            menuCanOpen = false;
            interactionPopUp.gameObject.SetActive(false);
        }
    }
}
