using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance;

    private PlayerControls controls;

    [Header("Movement")]
    public Vector2 move;
    public bool sprintHeld;

    public bool interactPressed;

    public bool jumpPressed;
    public bool dashPressed;
    public bool rollPressed;

    [Header("Stasis")]
    public bool stasisActivatePressed;
    public bool stasisSearchPressed;

    [Header("Mouse")]
    public bool firePressed;
    public bool aimPressed;

    [Header("Combat")]
    public bool reloadPressed;

    [Header("Util")]
    public bool pausePressed;
    public float horizontalBoatInputSmoothing = 10f;
    public float verticalBoatInputSmoothing = 10f;

    private bool canInput = true;
    //checking what scene we are in
    private Scene currentScene;

    public void ToggleInput(bool value)
    {
        canInput = value;
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }

        Instance = this;    

        controls = new PlayerControls();
    }
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    

    void Update()
    {
        if (!canInput)
            return;
        

        //disabling inputs based on scene, if it is any scene other than 4 (map scene) you are 3d and can use all abilities
        if(currentScene.buildIndex != 5)
        {
            /*sprintHeld = Input.GetKey(KeyCode.LeftShift);
            interactPressed = Input.GetKeyDown(KeyCode.F);
            reloadPressed = Input.GetKeyDown(KeyCode.R);
            pausePressed = Input.GetKeyDown(KeyCode.Escape);
            jumpPressed = Input.GetKeyDown(KeyCode.Space);
            firePressed = Input.GetMouseButton(0);
            aimPressed = Input.GetMouseButton(1);
            dashPressed = Input.GetKeyDown(KeyCode.LeftShift);
            rollPressed = Input.GetKeyDown(KeyCode.LeftControl);
            stasisActivatePressed = Input.GetMouseButton(0);
            stasisSearchPressed = Input.GetMouseButton(1);*/

            //sprintHeld = controls.Gameplay.Sprint.IsPressed();

            interactPressed = controls.Gameplay.Interact.WasPressedThisFrame();
            pausePressed = controls.Gameplay.Pause.WasPressedThisFrame();
            jumpPressed = controls.Gameplay.Jump.WasPressedThisFrame();
            dashPressed = controls.Gameplay.Dash.WasPressedThisFrame();
            rollPressed = controls.Gameplay.Roll.WasPressedThisFrame();

            stasisActivatePressed = controls.Gameplay.StasisFire.IsPressed();
            stasisSearchPressed = controls.Gameplay.StasisSearch.IsPressed();

            move = controls.Gameplay.Movement.ReadValue<Vector2>();
            if (move.sqrMagnitude > 1f) move.Normalize();
        }
        //if you are in map scene, you can only interact and pause as well as move
        else
        {
            interactPressed = controls.Gameplay.Interact.WasPressedThisFrame();
            pausePressed = controls.Gameplay.Pause.WasPressedThisFrame();

            float horizontal = controls.Gameplay.Movement.ReadValue<Vector2>().x;
            float vertical = controls.Gameplay.Movement.ReadValue<Vector2>().y;


            // Only allow movement with forward input
            if (vertical > 0f)
            {
                move = new Vector2(horizontal, vertical);

                if (move.sqrMagnitude > 1f)
                    move.Normalize();
            }
            else
            {
                //move = Vector2.Lerp(move, Vector2.zero, 10f * Time.deltaTime);
                move = new Vector2(Mathf.Lerp(move.x, 0, horizontalBoatInputSmoothing * Time.deltaTime), Mathf.Lerp(move.y, 0, verticalBoatInputSmoothing * Time.deltaTime));
            }
        }

        
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
}

