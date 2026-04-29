using UnityEngine;

public class MapScript : MonoBehaviour, IInteract
{
    public SceneHandler sceneHandler;

    private bool inCollider = false;
    [SerializeField] private string islandName;
    private string sceneName;
    public bool isHub = true;
    void Start()
    {
        sceneName = gameObject.name;
    }

    void Update()
    {
        //if(inCollider && playerInput.interactPressed)
        //{   

        //sceneHandler.LoadScene(sceneName);
        //Debug.Log("going");
        //}
    }

    public void OnInteract()
    {

        if (isHub)
        {
            GameManager.SetLastIsland("IslandHub");
            GameManager.VisitedHub();
        }


        sceneHandler.LoadScene(sceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            inCollider = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inCollider = false;
    }


}
