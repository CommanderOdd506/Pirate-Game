using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerInput : MonoBehaviour
{
    [Header("Movement")]
    public Vector2 move;
    public bool sprintHeld;

    public bool interactPressed;

    public bool jumpPressed;
    public bool dashPressed;
    public bool rollPressed;

    [Header("Mouse")]
    public bool firePressed;
    public bool aimPressed;

    [Header("Combat")]
    public bool reloadPressed;

    [Header("Util")]
    public bool pausePressed;


    private bool canInput = true;
    //checking what scene we are in
    private Scene currentScene;

    public void ToggleInput(bool value)
    {
        canInput = value;
    }

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    

    void Update()
    {
        if (!canInput)
            return;
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (move.sqrMagnitude > 1f) move.Normalize();

        //disabling inputs based on scene, if it is any scene other than 4 (map scene) you are 3d and can use all abilities
        if(currentScene.buildIndex != 4)
        {
            sprintHeld = Input.GetKey(KeyCode.LeftShift);
            interactPressed = Input.GetKeyDown(KeyCode.F);
            reloadPressed = Input.GetKeyDown(KeyCode.R);
            pausePressed = Input.GetKeyDown(KeyCode.Escape);
            jumpPressed = Input.GetKeyDown(KeyCode.Space);
            firePressed = Input.GetMouseButton(0);
            aimPressed = Input.GetMouseButton(1);
            dashPressed = Input.GetKeyDown(KeyCode.LeftShift);
            rollPressed = Input.GetKeyDown(KeyCode.LeftControl);
        }
        //if you are in map scene, you can only interact and pause as well as move
        else
        {
            interactPressed = Input.GetKeyDown(KeyCode.F);
            pausePressed = Input.GetKeyDown(KeyCode.Escape);
        }

        
    }
}
