using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public TimeManager timeManager;

	public void LoadScene(string name)
	{
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadSceneAsync(name);
    }
	public void HubWorld()
	{
        LoadScene("Hub World");
	}

	public void LevelOne()
	{
        LoadScene("Level One");
	}

	public void LevelTwo()
	{
        LoadScene("Level Two");
	}

	public void LevelThree()
	{
        LoadScene("Level Three");
	}

	public void MapWorld()
	{
        LoadScene("Map World");
	}

	public void Settings()
	{
		LoadScene("Settings");
	}

	public void MainMenu()
	{
		LoadScene("Main Menu");
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
        SceneManager.sceneLoaded -= OnSceneLoaded;
        timeManager = FindObjectOfType<TimeManager>();
        if (timeManager != null)
        {
            timeManager.Resume();
        }

		//locks or unlocks cursor after load
		if (scene.name == "Settings" || scene.name == "Main Menu") //settings + main menu
		{
			MouseUnlock();
		}
		else
		{
			MouseLock();
		}
	}

	void MouseUnlock()
	{
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	void MouseLock()
	{
		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
