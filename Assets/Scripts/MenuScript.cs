using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
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
}
