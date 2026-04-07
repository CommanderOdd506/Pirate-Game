using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public PlayerInput playerInput;
	private bool isPaused = false;
	public GameObject returnButton;

	private void Start()
	{
		returnButton.SetActive(false);
	}

	private void Update()
	{
		if(playerInput.pausePressed)
		{
			if(isPaused)
				Resume();

			else
				Pause();
		}
	}

	private void Pause()
	{
		Time.timeScale = 0f;
		isPaused = true;

		returnButton.SetActive(true);

		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	private void Resume()
	{
		Time.timeScale = 1;
		isPaused = false;

		returnButton.SetActive(false);

		Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}

	public void HubWorld()
	{
		SceneManager.LoadSceneAsync(0);

		Resume();
	}

	public void LevelOne()
	{
		SceneManager.LoadSceneAsync(1);

		Resume();
	}

	public void LevelTwo()
	{
		SceneManager.LoadSceneAsync(2);

		Resume();
	}

	public void LevelThree()
	{
		SceneManager.LoadSceneAsync(3);

		Resume();
	}

	public void MapWorld()
	{
		SceneManager.LoadSceneAsync(4);

		Resume();
	}

	public void Settings()
	{
		SceneManager.LoadSceneAsync(5);

		Resume();

		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	public void MainMenu()
	{
		SceneManager.LoadSceneAsync(6);

		Resume();

		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	public void QuitGame()
	{
		Application.Quit();
	}

}
