using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public string mainMenuScene = "MainMenu";

    private PlayerInput playerInput;
    public GameObject pausePanel;
    public GameObject deathPanel;

    public GameObject mainPage;
    public GameObject settingsPage;
    public GameObject controlsPanel;

    public GameObject firstButtonOnSettingsPage;
    public GameObject firstButtonOnMainPage;
    public GameObject firstButtonOnControlsPage;

    private bool paused;

    public static Action OnPause;
    public static Action OnResume;
    
    public bool IsPaused => paused;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        Instance = this;
        ResumeGame();

    }
    void Update()
    {
        if (!PlayerInput.Instance.pausePressed) return;

        if (IsPaused)
            ResumeGame();
        else
            PauseGame();
    }


    public void OpenSettingsPage()
    {
        mainPage.SetActive(false);
        if(controlsPanel) controlsPanel.SetActive(false);
        if (settingsPage) settingsPage.SetActive(true);


        // Clear selection first
        EventSystem.current.SetSelectedGameObject(null);

        // Set new selected button
        EventSystem.current.SetSelectedGameObject(firstButtonOnSettingsPage);
    }

    public void OpenControlsPage()
    {
        mainPage.SetActive(false);
        controlsPanel.SetActive(true);
        settingsPage.SetActive(false);


        // Clear selection first
        EventSystem.current.SetSelectedGameObject(null);

        // Set new selected button
        EventSystem.current.SetSelectedGameObject(firstButtonOnControlsPage);
    }

    public void OpenMainPage()
    {
        mainPage.SetActive(true);
        if (controlsPanel) controlsPanel.SetActive(false);
        if (settingsPage) settingsPage.SetActive(false);

        EventSystem.current.SetSelectedGameObject(null);

        // Set new selected button
        EventSystem.current.SetSelectedGameObject(firstButtonOnMainPage);
    }

    public void PauseGame()
    {
        if (!paused)
        {
             Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            paused = true;
            OnPause?.Invoke();
            if (pausePanel != null)
                pausePanel.SetActive(true);
            OpenMainPage();

           
        }
    }

    public void ResumeGame()
    {
        if (paused)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            paused = false;
            OnResume?.Invoke();
            if (pausePanel != null)
                pausePanel.SetActive(false);

            
        }
    }

    public void Retry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }
}

