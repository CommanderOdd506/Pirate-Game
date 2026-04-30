using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipInteractable : MonoBehaviour, IInteract
{
    public SceneHandler sceneHandler;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void OnInteract()
    {
        sceneHandler.LoadScene("Outro Cutscene");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
