//Hanul Huh
using MiscUtil.Collections.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //Variables live here
    #region variables 
    [SerializeField] GameObject inventoryPanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject characterPanel;
    [SerializeField] GameObject loadPanel;
    [SerializeField] GameObject savePanel;

    [SerializeField] Text money;
    [SerializeField] Text xp;

    [SerializeField] Button resume;
    [SerializeField] Button character;
    [SerializeField] Button inventory;
    [SerializeField] Button save;
    [SerializeField] Button load;
    [SerializeField] Button settings;
    [SerializeField] Button quit;

    [SerializeField] Button loadSave1;
    [SerializeField] Button loadSave2;
    [SerializeField] Button loadSave3;
    [SerializeField] Button saveSlot1;
    [SerializeField] Button saveSlot2;
    [SerializeField] Button saveSlot3;

    public GameObject menuButtons;

    bool pause = false;
    Player player;
    #endregion      
    void Start() {  //Ensures none of the Pause Menu UI elements are active at the start and that the time scale is set to 1
        Unpause();
    }
    void Update() {
        if (!pause)
            if (Input.GetKeyDown(KeyCode.I) && !IsShopping.shopping) {
                Pause();
                Inventory();
            } else if (Input.GetKeyDown(KeyCode.P) && !IsShopping.shopping) {
                Pause();
                Character();
            } else if (Input.GetKeyDown(KeyCode.Escape) && !IsShopping.shopping || Input.GetKeyDown(KeyCode.O) && !IsShopping.shopping) {
                Pause();
                Settings();
            }
            else {
                if (Input.GetKeyDown(KeyCode.I) && !IsShopping.shopping) {
                    Inventory();
                } else if (Input.GetKeyDown(KeyCode.P) && !IsShopping.shopping) {
                    Character();
                } else if (Input.GetKeyDown(KeyCode.O) && !IsShopping.shopping) {
                    Settings();
                } else if (Input.GetKeyDown(KeyCode.Escape) && !IsShopping.shopping) {
                    Unpause();
                }
            }
    }

    public void Pause() { //Inventory, Character, and Settings currently depicted by panels. Will need to replace with proper assets when available. Could probably just have 1 panel and change its color based on which menu you're observing.
        pause = true;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        inventoryPanel.SetActive(true);
        menuButtons.SetActive(true);
        xp.text = "Ekspee: 999,999"; //Set to player xp variable
        money.text = "Muhnie: 999,999"; //Set to player money variable
    }
    public void Unpause() {     //Unpauses the game and disables all Pause Menu UI elements
        pause = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        characterPanel.SetActive(false);
        settingsPanel.SetActive(false);
        menuButtons.SetActive(false);
        savePanel.SetActive(false);       
        loadPanel.SetActive(false);        
        xp.text = string.Empty; 
        money.text = string.Empty;       
    }
    #region Button Methods
    public void Character() {       //Enables Character UI, disables other UI elements
        inventoryPanel.SetActive(false);
        settingsPanel.SetActive(false);
        characterPanel.SetActive(true);
        savePanel.SetActive(false);
        loadPanel.SetActive(false);
    }
    public void Inventory() {       //Eables Inventory UI, disables other UI elements
        characterPanel.SetActive(false);
        settingsPanel.SetActive(false);
        inventoryPanel.SetActive(true);
        savePanel.SetActive(false);
        loadPanel.SetActive(false);
    }
    public void Settings() {        //Eables Settings UI, disables other UI elements
        characterPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        settingsPanel.SetActive(true);
        savePanel.SetActive(false);
        loadPanel.SetActive(false);
    }
    public void SaveGame() {        //Eables Save Game UI, disables other UI elements
        savePanel.SetActive(true);
        loadPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        settingsPanel.SetActive(false);
        characterPanel.SetActive(false);
    }
    public void LoadGame() {        //Eables Load Game UI, disables other UI elements       
        loadPanel.SetActive(true);
        savePanel.SetActive(false);
        inventoryPanel.SetActive(false);
        settingsPanel.SetActive(false);
        characterPanel.SetActive(false);
    }
    public void MainMenu() {        //Returns to Main Menu
        Unpause();
        //SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame() {        //Quits the game without saving
        Unpause();
        Application.Quit();
    }

    //Scene loading is initated here, all other player elements will be loaded once the new scene has initialized by using the StaticLoad.loading boolean.
    public void LoadSave1() {       //Loads Save Slot 1
        StaticLoad.saveSlot = 1;
        StaticLoad.loading = true;
        player = XMLOp.Deserialize<Player>("Save1.xml");
        SceneManager.LoadScene(player.scene);
    }
    public void LoadSave2() {       //Loads Save Slot 2             
        StaticLoad.saveSlot = 2;
        StaticLoad.loading = true;
        player = XMLOp.Deserialize<Player>("Save2.xml");
        SceneManager.LoadScene(player.scene);
    }
    public void LoadSave3() {       //Loads Save Slot 3
        StaticLoad.saveSlot = 3;
        StaticLoad.loading = true;
        player = XMLOp.Deserialize<Player>("Save3.xml");
        SceneManager.LoadScene(player.scene);
    }
    #endregion
}
