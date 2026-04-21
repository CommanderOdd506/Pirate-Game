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
        if(canInteract & playerInput.interactPressed)
        {
            //if currentinteractable is not null call the function
            currentInteractable?.OnInteract();
        }
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

            promptUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInteract interactable = other.GetComponent<IInteract>();

        if (interactable != null && interactable == currentInteractable)
        {
            canInteract = false;
            currentInteractable = null;

            promptUI.gameObject.SetActive(false);
        }
    }
}
