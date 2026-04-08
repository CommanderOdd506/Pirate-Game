using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DELETELATERCanonscript : MonoBehaviour
{
    public Transform loadPoint;
    public Transform landingPoint;
    public float launchHeight = 5f;
    public KeyCode shootKey = KeyCode.Space;

    public GameObject interactUI;
    public TextMeshProUGUI interactText;

    private GameObject player;
    private Rigidbody playerRb;
    private MonoBehaviour playerMovement;

    private bool playerInside = false;
    private bool isLoaded = false;
    private bool isFlying = false;

    private LineRenderer line;
    private CharacterController controller;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = true;

        if (interactUI != null)
            interactUI.SetActive(false);
    }

    private void Update()
    {
        if (isLoaded)
        {
            DrawArc();

            // Show UI while loaded
            if (interactUI != null)
            {
                interactUI.SetActive(true);
                interactText.text = "Press " + shootKey + " to shoot";
            }

            if (Input.GetKeyDown(shootKey))
            {
                ShootPlayer();
            }
        }
        else
        {
            if (interactUI != null)
                interactUI.SetActive(false);
        }
    }

    void LoadPlayer()
    {
        isLoaded = true;

        controller.enabled = false;

        playerRb.velocity = Vector3.zero;
        playerRb.isKinematic = true;

        player.transform.position = loadPoint.position;
        player.transform.SetParent(loadPoint);

        TogglePlayerControl(false);

        line.enabled = true;
    }

    void ShootPlayer()
    {
        isLoaded = false;
        isFlying = true;
        playerInside = false;

        player.transform.SetParent(null);

        controller.enabled = false;

        playerRb.isKinematic = false;
        playerRb.velocity = Vector3.zero;
        playerRb.velocity = CalculateLaunchVelocity();

        line.enabled = false;

        StartCoroutine(CheckLanding());
    }

    IEnumerator CheckLanding()
    {
        yield return new WaitForSeconds(0.5f);

        while (!IsGrounded())
        {
            yield return null;
        }

        playerRb.velocity = Vector3.zero;
        playerRb.isKinematic = true;

        controller.enabled = true;

        TogglePlayerControl(true);
        isFlying = false;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(player.transform.position, Vector3.down, 1.2f);
    }

    Vector3 CalculateLaunchVelocity()
    {
        Vector3 start = loadPoint.position;
        Vector3 end = landingPoint.position;

        float gravity = Physics.gravity.y;
        float time = 1.5f;

        Vector3 displacement = end - start;

        Vector3 velocityXZ = new Vector3(displacement.x, 0, displacement.z) / time;
        float velocityY = (displacement.y - 0.5f * gravity * time * time) / time;

        return velocityXZ + Vector3.up * velocityY;
    }

    void DrawArc()
    {
        Vector3 velocity = CalculateLaunchVelocity();
        Vector3 start = loadPoint.position;

        int resolution = 30;
        float time = 1.5f;
        float timeStep = time / resolution;

        line.positionCount = resolution;

        for (int i = 0; i < resolution; i++)
        {
            float t = i * timeStep;
            Vector3 point = start + velocity * t + 0.5f * Physics.gravity * t * t;
            line.SetPosition(i, point);
        }
    }

    void TogglePlayerControl(bool enabled)
    {
        if (playerMovement != null)
            playerMovement.enabled = enabled;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLoaded && !isFlying)
        {
            playerInside = true;
            player = other.gameObject;
            playerRb = player.GetComponent<Rigidbody>();
            playerMovement = player.GetComponent<PlayerMovement>(); 
            controller = player.GetComponent<CharacterController>();

            LoadPlayer(); // ?? Auto load on enter
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isLoaded)
        {
            playerInside = false;

            if (interactUI != null)
                interactUI.SetActive(false);
        }
    }
}

