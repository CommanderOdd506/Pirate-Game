using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public TimeManager timeManager;

	public void HubWorld()
	{
		SceneManager.LoadSceneAsync(0);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void LevelOne()
	{
		SceneManager.LoadSceneAsync(1);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void LevelTwo()
	{
		SceneManager.LoadSceneAsync(2);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void LevelThree()
	{
		SceneManager.LoadSceneAsync(3);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void MapWorld()
	{
		SceneManager.LoadSceneAsync(4);

		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void Settings()
	{
		SceneManager.LoadSceneAsync(5);

		SceneManager.sceneLoaded += OnSceneLoaded;

		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	public void MainMenu()
	{
		SceneManager.LoadSceneAsync(6);

		SceneManager.sceneLoaded += OnSceneLoaded;

		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		timeManager = FindObjectOfType<TimeManager>();

		timeManager.Resume();
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
