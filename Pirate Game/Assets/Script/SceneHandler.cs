using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
	public void HubWorld()
	{
		SceneManager.LoadSceneAsync(0);
	}

	public void LevelOne()
	{
		SceneManager.LoadSceneAsync(1);
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	public void LevelTwo()
	{
		SceneManager.LoadSceneAsync(2);
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	public void LevelThree()
	{
		SceneManager.LoadSceneAsync(3);
		Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}

	public void MapWorld()
	{
		SceneManager.LoadSceneAsync(4);
	}

}
