using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Start()
    {

		SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
	}

    public void PlayGame()
    {
		SceneManager.LoadScene("PlayGame");
	}

    public void PlayMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
