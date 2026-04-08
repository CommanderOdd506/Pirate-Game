using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public TimeManager timeManager;

	public void LoadScene(int index)
	{
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(index);
    }
	public void HubWorld()
	{
        LoadScene(0);
	}

	public void LevelOne()
	{
        LoadScene(1);
	}

	public void LevelTwo()
	{
        LoadScene(2);
	}

	public void LevelThree()
	{
        LoadScene(3);
	}

	public void MapWorld()
	{
        LoadScene(4);
	}

	public void Settings()
	{
		LoadScene(5);
	}

	public void MainMenu()
	{
		LoadScene(6);
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
        SceneManager.sceneLoaded -= OnSceneLoaded;
        timeManager = FindObjectOfType<TimeManager>();
        if (timeManager != null)
        {
            timeManager.Resume();
        }
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
