using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject How2;
    public void OnPlay()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnQuit()
    {
        Application.Quit();
    }
    public void OnMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void HowTo()
    {
        How2.SetActive(true);
        MainMenu.SetActive(false);
    }
    public void RevertToMenu()
    {
        How2.SetActive(false);
        MainMenu.SetActive(true);
    }
}
