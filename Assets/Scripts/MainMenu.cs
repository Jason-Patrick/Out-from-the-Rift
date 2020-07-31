using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject creditsMenu;

    public void Awake()
    {
        if (creditsMenu == null) return;
        creditsMenu.SetActive(false);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("OotR");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
