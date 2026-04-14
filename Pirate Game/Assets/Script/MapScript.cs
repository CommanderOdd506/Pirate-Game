using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour, IInteract
{
    public SceneHandler sceneHandler;

    private bool inCollider = false;
    private string sceneName;
    void Start()
    {
        sceneName = gameObject.name;
    }

    void Update ()
    {
        //if(inCollider && playerInput.interactPressed)
        //{   

             //sceneHandler.LoadScene(sceneName);
             //Debug.Log("going");
        //}
    }

    public void OnInteract()
    {
        sceneHandler.LoadScene(sceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            inCollider = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            inCollider = false;
    }

    
}
