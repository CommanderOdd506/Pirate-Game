using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapScript : MonoBehaviour
{
    public PlayerInput playerInput;
    public SceneHandler sceneHandler;

    private bool inCollider = false;
    private string objectName;
    void Start()
    {
        objectName = gameObject.name;
    }

    void Update ()
    {
        if(inCollider && playerInput.interactPressed)
        {   
             Debug.Log(objectName);
             switch (objectName)
             {
                 case "Map":
                    sceneHandler.MapWorld();
                    break;

                 case "Level One":
                    sceneHandler.LevelOne();
                    break;

                 case "Level Two":
                    sceneHandler.LevelTwo();
                    break;

                 case "Level Three":
                    sceneHandler.LevelThree();
                    break;

                 case "Hub":
                    sceneHandler.HubWorld();
                    break;
             }

        }
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
