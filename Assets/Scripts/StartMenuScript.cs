using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void QuitGame()
    {
        //SceneManager.LoadScene("Start Menu");
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
