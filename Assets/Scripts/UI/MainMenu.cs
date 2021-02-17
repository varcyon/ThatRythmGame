using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//Created by Mike Lecus

public class MainMenu : MonoBehaviour
{
    
    public void NewGame()
    {
        SceneManager.LoadScene("Overworld");        //New game loads the Overworld scene
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Quit works with a build version.");  //Just to make sure the button works
    }
}
