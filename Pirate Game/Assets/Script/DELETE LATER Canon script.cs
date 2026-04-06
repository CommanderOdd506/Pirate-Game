using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DELETELATERCanonscript : MonoBehaviour
{
    public Transform loadPoint;
    public Transform landingPoint;
    public float launchHeight = 5f;
    public KeyCode interactKey = KeyCode.E;
    public KeyCode shootKey = KeyCode.Space;
   
    private GameObject player;
    private Rigidbody playerRb;
    private MonoBehaviour playerMovement;
    
    private bool playerInside = false;
    private bool isLoaded = false;
    private bool isFlying = false;

    private LineRenderer line;

    CharacterController controller;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = true;
    }
    private void Update()
    {
        if (playerInside && !isLoaded && Input.GetKeyDown(interactKey))
        {
            LoadPlayer();
        }
        if (isLoaded)
        {
            DrawArc();

            if (isLoaded && Input.GetKeyDown(shootKey))
            {
                ShootPlayer();
            }
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

        line.enabled=true;
    }
    void ShootPlayer()
    {
        isLoaded = false;
        isFlying = true;

        playerInside = false;

        player.transform.SetParent(null);

        controller.enabled = false;

        playerRb.isKinematic =false;
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
        isFlying=false;
        
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

        for(int i = 0; i < resolution; i++)
        {
            float t = i * timeStep;
            Vector3 point = start + velocity * t + 0.5f * Physics.gravity * t * t;
            line.SetPosition(i, point);
        }
    }
    void TogglePlayerControl( bool enabled )
    {
        MonoBehaviour PlayerMovement = player.GetComponent<MonoBehaviour>();
        if ( PlayerMovement != null )
            PlayerMovement.enabled = enabled;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            player = other.gameObject;
            playerRb = player.GetComponent<Rigidbody>();

            playerMovement = player.GetComponent<MonoBehaviour>();

            controller = player.GetComponent<CharacterController>();    
        }
    }
    private void OnTriggerExit(Collider other)
    {
       if (other.CompareTag("Player") && !isLoaded)
        {
            playerInside = false;
        }
    }
}
