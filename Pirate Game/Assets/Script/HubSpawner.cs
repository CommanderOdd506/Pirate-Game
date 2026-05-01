using UnityEngine;

public class HubSpawner : MonoBehaviour
{

    public Transform player;
    public Transform visitedSpawn;
    public Checkpoint startingCheckpoint;
    public GameObject tutorialObject;

    private void Start()
    {
        bool visited = GameManager.HasVisitedHub();
        if (visited)
        {
            Teleport();
            CheckpointManager.Instance.SetCheckpoint(startingCheckpoint);
            tutorialObject.SetActive(false);
        }
    }

    private void Teleport()
    {
        CharacterController characterController = player.gameObject.GetComponent<CharacterController>();

        characterController.enabled = false;
        player.position = visitedSpawn.position;
        player.rotation = visitedSpawn.rotation;
        characterController.enabled = true;
    }
}
