using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class XMLSaveLoad : MonoBehaviour
{
    Player player = new Player();
    int saveSlot;
    // Start is called before the first frame update
    void Start() {
        saveSlot = StaticLoad.saveSlot;
        if(StaticLoad.loading == true) {
            if (System.IO.File.Exists("Save" + saveSlot + ".xml")) {
                player = XMLOp.Deserialize<Player>("Save" + saveSlot + ".xml");
            } else {
                player.name = "Jonas";                              //
                player.hp = 100;                                    //
                player.atk = 5;                                     //
                player.def = 5;                                     //
                player.accuracy = 1.0f;                             //
                player.efficiency = 1.0f;                           //      Set these values to the new character defaults
                player.level = 1;                                   //
                Vector3 playerPos = new Vector3(0f, 0f, 0f);        //
                player.posx = playerPos.x;                          //
                player.posy = playerPos.y;                          //
                player.posz = playerPos.z;                          //
                player.scene = SceneManager.GetActiveScene().name;  //
            }
        } else {
            player.name = "Jonas";                              //
            player.hp = 100;                                    //
            player.atk = 5;                                     //
            player.def = 5;                                     //
            player.accuracy = 1.0f;                             //
            player.efficiency = 1.0f;                           //      Set these values to the new character defaults
            player.level = 1;                                   //
            Vector3 playerPos = new Vector3(0f, 0f, 0f);        //
            player.posx = playerPos.x;                          //
            player.posy = playerPos.y;                          //
            player.posz = playerPos.z;                          //
            player.scene = SceneManager.GetActiveScene().name;  //
        }
    }
    
    public void SaveSlot1() {
        XMLOp.Serialize(player, "Save1.xml");
    }
    public void SaveSlot2() {
        XMLOp.Serialize(player, "Save2.xml");
    }
    public void SaveSlot3() {
        XMLOp.Serialize(player, "Save3.xml");
    }
}
