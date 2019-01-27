using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject credits;
    public GameObject brief;

    public Button startGame;
    public Button showCredits;
    public Button quitGame;
    public Button backButton;

    private void Awake()
    {
        mainMenu.SetActive(true);
        credits.SetActive(false);

        startGame.onClick.AddListener(StartGame);
        showCredits.onClick.AddListener(ShowCredits);
        quitGame.onClick.AddListener(QuitGame);
        backButton.onClick.AddListener(ShowMainMenu);
    }

    public void StartGame()
    {
        //SceneManager.LoadScene("game");
        mainMenu.SetActive(false);
        brief.SetActive(true);
    }

    public void ShowCredits()
    {
        this.mainMenu.SetActive(false);
        this.credits.SetActive(true);
    }

    public void ShowMainMenu()
    {
        this.credits.SetActive(false);
        this.mainMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
