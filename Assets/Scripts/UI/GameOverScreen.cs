using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Created by Mike Lecus
public class GameOverScreen : MonoBehaviour
{
    public void ContinueButton()                //This will bring you to the Overworld when clicking the "Continue" Button
    {
        SceneManager.LoadScene("Overworld Test Area");
    }

    public void QuitButton()                    //This will quit Application when running a build of the project
    {
        Application.Quit();
        Debug.Log("Quit works with a build version.");  //Just to make sure the button works
    }
}
