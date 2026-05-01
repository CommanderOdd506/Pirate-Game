using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public GameObject firstButtonOnMainPage;
    // Start is called before the first frame update
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstButtonOnMainPage);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
