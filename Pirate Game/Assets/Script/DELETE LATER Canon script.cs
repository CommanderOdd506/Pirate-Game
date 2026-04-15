using System.Collections;
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
    private CharacterController controller;
    private MonoBehaviour playerMovement;

    private bool isLoaded = false;
    private bool isFlying = false;

    private LineRenderer line;

    private Vector3 currentVelocity;
    private float gravity = -9.81f;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = true;

        if (interactUI != null)
            interactUI.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (isLoaded)
        {
            DrawArc();

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

        if (isFlying)
        {
            HandleFlight();
        }
    }

    void LoadPlayer()
    {
        isLoaded = true;

        controller.enabled = false;

        player.transform.position = loadPoint.position;
        player.transform.SetParent(loadPoint);

        TogglePlayerControl(false);

        line.enabled = true;
    }

    void ShootPlayer()
    {
        isLoaded = false;
        isFlying = true;

        player.transform.SetParent(null);

        controller.enabled = true;

        currentVelocity = CalculateLaunchVelocity();

        line.enabled = false;
    }

    void HandleFlight()
    {
        currentVelocity.y += gravity * Time.deltaTime;

        controller.Move(currentVelocity * Time.deltaTime);

        if (controller.isGrounded && currentVelocity.y <= 0)
        {
            isFlying = false;
            currentVelocity = Vector3.zero;

            TogglePlayerControl(true);
        }
    }

    Vector3 CalculateLaunchVelocity()
    {
        Vector3 start = loadPoint.position;
        Vector3 end = landingPoint.position;

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
            Vector3 point = start + velocity * t + 0.5f * Vector3.up * gravity * t * t;
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
            player = other.gameObject;
            controller = player.GetComponent<CharacterController>();
            playerMovement = player.GetComponent<PlayerMovement>();

            LoadPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isLoaded)
        {
            if (interactUI != null)
                interactUI.SetActive(false);
        }
    }
}