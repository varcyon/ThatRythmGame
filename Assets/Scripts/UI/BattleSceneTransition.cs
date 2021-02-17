using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Created by Mike Lecus
public class BattleSceneTransition : MonoBehaviour{
    public void BattleSceneButton(){
        SceneManager.LoadScene("BattleTestScene");
    }
}
