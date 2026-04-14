using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionManager : MonoBehaviour
{

    public PlayerInput playerInput;
    public SceneManager sceneManager;

    public bool canInteract = false;
    private string objectName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteract & playerInput.interactPressed)
        {
            Debug.Log(objectName);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Map"))
        {
            canInteract = true;
            objectName = other.gameObject.name;
        }

        else if(other.CompareTag("Level"))
        {
            canInteract = true;
            objectName = other.gameObject.name;
        }

        else if(other.CompareTag("Collectible"))
        {
            canInteract = true;
            objectName = other.gameObject.name;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canInteract = false;
            objectName = null;
        }
    }
}
