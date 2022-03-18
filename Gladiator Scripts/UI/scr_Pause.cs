using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_Pause : MonoBehaviour
{

    GameObject em;
    GameObject ui;

    private void Start()
    {

        //Creating and transforming the ui. Also adds listeners to buttons so that they have functions

        em = GameObject.Find("EntityManager");
        var prefabButton = Resources.Load("UI/Button");

        ui = new GameObject("Pause");
        GameObject canvas = GameObject.Find("Canvas");
        ui.transform.SetParent(canvas.transform);
        ui.AddComponent<RectTransform>();
        ui.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;

        GameObject but = (GameObject)Instantiate(prefabButton, new Vector3(0, 0, 0), Quaternion.identity);
        Button resume = but.GetComponent<Button>();
        resume.transform.SetParent(ui.transform);
        resume.GetComponentInChildren<Text>().text = "Resume";
        resume.GetComponent<RectTransform>().anchoredPosition= new Vector3(0,75,0);
        resume.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 70);
        resume.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        resume.onClick.AddListener(Resume);

        GameObject butt = (GameObject)Instantiate(prefabButton, new Vector3(0, 0, 0), Quaternion.identity);
        Button menu = butt.GetComponent<Button>();
        menu.transform.SetParent(ui.transform);
        menu.GetComponentInChildren<Text>().text = "Main Menu";
        menu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -75, 0);
        menu.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 70);
        menu.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        menu.onClick.AddListener(MainMenu);

        //Starts the ui hidden
        ui.SetActive(false);
    }

    private void Update()
    {

        //If the player clicks "esc" then they cant move, the enemies are frozen and shows the ui, the cursor and unlocks is
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            em.GetComponent<Player>().canMove = false;
            em.GetComponent<Human>().paused = true;
            ui.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    //If a button is pressed, unfreezes everyone, hides ui and locks and hides cursor back
    public void Resume()
    {
        em.GetComponent<Player>().canMove = true;
        em.GetComponent<Human>().paused = false;
        ui.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //If a button is pressed, loads the scene "MainMenu", basically quitting the game.
    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
