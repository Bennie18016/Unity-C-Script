using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuOptions : MonoBehaviour
{
    //Function start game
    public void startGame()
    {
        //Loads the scene "DevMap"
        SceneManager.LoadScene("DevMap");
    }

    //Function QuitGame
    public void quitGame()
    {
        //Quits the application (if your in a build)
        Application.Quit();
    }

}
