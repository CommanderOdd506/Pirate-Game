using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InteractionManager : MonoBehaviour
{

    public PlayerInput playerInput;
    public SceneManager sceneManager;

    public TextMeshProUGUI promptUI;

    public bool canInteract = false;
    private IInteract currentInteractable;

    void Start()
    {
        promptUI.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HandleInteraction();
        PauseLogicUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        //finds interactable interface on script of gameobject whose collider you are entering
        IInteract interactable = other.GetComponent<IInteract>();


        if (interactable != null)
        {
            //you can interact if it exists
            canInteract = true;
            currentInteractable = interactable;

            promptUI.text = $"Press \"F\" to interact with {other.gameObject.name}";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteract interactable = other.GetComponent<IInteract>();

        if (interactable != null && interactable == currentInteractable)
        {
            canInteract = false;
            currentInteractable = null;
        }
    }

    private void HandleInteraction()
    {
        if(canInteract && playerInput.interactPressed)
        {
            //if currentinteractable is not null call the function
            currentInteractable?.OnInteract();
        }
    }

    private void PauseLogicUI()
    {
        if (canInteract && !PauseMenu.Instance.IsPaused)
        {
            promptUI.gameObject.SetActive(true);
        }
        else if (canInteract && PauseMenu.Instance.IsPaused)
        {
            promptUI.gameObject.SetActive(false);
        }
    }
}
