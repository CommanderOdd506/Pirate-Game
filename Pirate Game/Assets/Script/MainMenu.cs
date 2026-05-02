using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public GameObject firstButtonOnMainPage;

    public GameObject gameImage;

    public GameObject controlsPage;
    public GameObject MainPage;

    public GameObject returnButton;

    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstButtonOnMainPage);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ControlsButton()
    {
        MainPage.SetActive(false);

        gameImage.SetActive(false);

        controlsPage.SetActive(true);

        EventSystem.current.SetSelectedGameObject(returnButton);
    }

    public void ReturnToMain()
    {
        controlsPage.SetActive(false);

        gameImage.SetActive(true);

        MainPage.SetActive(true);

        EventSystem.current.SetSelectedGameObject(firstButtonOnMainPage);
    }
}
