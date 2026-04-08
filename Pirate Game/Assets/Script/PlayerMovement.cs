using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform camera;

    public bool canSprint = false;
    [Header("Speed")]
    public float walkSpeed = 4.5f;
    public float sprintSpeed = 6.5f;

    [Header("Jump / Gravity")]
    public float groundStickForce = -5f;
    public float gravity = -30f;
    public float jumpHeight = 1.1f;
    public float jumpBuffer = 0.12f;
    public float coyoteTime = 0.12f;

    [Header("Rotation")]
    public float rotationSpeed = 10f;

    [Header("Dash")]
    public float dashSpeed = 12f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 0.5f;

    [Header("Roll")]
    public float rollSpeed = 8f;
    public float rollDuration = 0.6f;
    public float rollCooldown = 1f;

    //runtime
    private PlayerInput input;
    private Vector3 _velocity;
    private float _currentSpeed;
    private float _timeSinceLeftGround;
    private float _timeSinceJumpPressed;
    private bool isGrounded;
    private bool isSprinting;
    private bool isDashing;
    private bool isRolling;
    private float dashTimer;
    private float rollTimer;
    private float dashCooldownTimer;
    private float rollCooldownTimer;
    private Vector3 abilityDirection;
    private bool inMapScene;
    private bool canDoubleJump;

    void Awake()
    {
        if (!controller) controller = GetComponent<CharacterController>();
        _timeSinceJumpPressed = jumpBuffer + 0.01f;
    }

    void Start()
    {
        input = GetComponent<PlayerInput>();
        Cursor.lockState = CursorLockMode.Locked;
        if(IsInMapScene())
        {
            inMapScene = true;
        }
    }



    bool IsInMapScene()
    {
        return SceneManager.GetActiveScene().name == "MapScene";
    }

void Update()
    {
        //Normalize forward and right vectors to remove vertical direction and properly scale horizontal
        //use cam as a the direction reference for 3d platformer environment 
        Vector3 camForward = camera.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = camera.right;
        camRight.y = 0f;
        camRight.Normalize();


        //Collect Input
        isGrounded = controller.isGrounded;
        Vector2 move = input.move;
        bool sprintHeld = input.sprintHeld;
        bool jumpPressed = input.jumpPressed;

        //timers 

        if (jumpPressed) _timeSinceJumpPressed = 0f; else _timeSinceJumpPressed += Time.deltaTime;

        if (isGrounded) _timeSinceLeftGround = 0f; else _timeSinceLeftGround += Time.deltaTime;

        // cooldown timers
        dashCooldownTimer -= Time.deltaTime;
        rollCooldownTimer -= Time.deltaTime;

       if(isGrounded && !canDoubleJump)
        {
            canDoubleJump = true;
        }

        //horizontal move

        Vector3 moveDir = camForward * move.y + camRight * move.x;
        if (moveDir.sqrMagnitude > 1f) moveDir.Normalize();
        Vector3 horizontal = new Vector3();

        // DASH
        if (input.dashPressed && dashCooldownTimer <= 0f && !isDashing && !isRolling)
        {
            StartDash(moveDir);
        }

        // ROLL
        if (input.rollPressed && rollCooldownTimer <= 0f && !isRolling && !isDashing)
        {
            StartRoll(moveDir);
        }

        //horizontal move vector and dash-roll implementation
        if (isDashing)
        {
            dashTimer -= Time.deltaTime;
            horizontal = abilityDirection * dashSpeed;

            if (dashTimer <= 0f)
                isDashing = false;
        }
        else if (isRolling)
        {
            rollTimer -= Time.deltaTime;
            horizontal = abilityDirection * rollSpeed;

            if (rollTimer <= 0f)
                isRolling = false;
        }
        else
        {
            horizontal = moveDir * _currentSpeed;
        }

        //sprint check
        isSprinting = sprintHeld && moveDir.magnitude > 0.1f;

        float targetSpeed = isSprinting && canSprint ? sprintSpeed : walkSpeed;
        targetSpeed *= Mathf.Clamp01(move.magnitude);

        //apply speed
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, targetSpeed, 10 * Time.deltaTime);

       
        //timer check bools 
        bool canCoyoteJump = _timeSinceLeftGround <= coyoteTime;
        bool bufferedJump = _timeSinceJumpPressed <= jumpBuffer;
        if (bufferedJump && (isGrounded || canCoyoteJump))
        {
            _timeSinceJumpPressed = jumpBuffer + 1f;
            _velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }
        else if(bufferedJump && canDoubleJump)
        {
            canDoubleJump = false;
            _timeSinceJumpPressed = jumpBuffer + 1f;
            _velocity.y = Mathf.Sqrt(-2f * gravity * jumpHeight);
        }

        if (isGrounded && _velocity.y < 0f) _velocity.y = groundStickForce;
        else _velocity.y += gravity * Time.deltaTime;

        //rotate towards move direction
        if (moveDir.sqrMagnitude > 0.01f && !isDashing)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }


        //cancel gravity
        if (isDashing)
        {
            _velocity.y = 0;
        }

        Vector3 motion = horizontal + new Vector3(0f, _velocity.y, 0f);
        controller.Move(motion * Time.deltaTime);
    }



    //ABILITIES


    void StartDash(Vector3 moveDir)
    {
        isDashing = true;
        dashTimer = dashDuration;
        dashCooldownTimer = dashCooldown;

        abilityDirection = moveDir.sqrMagnitude > 0.01f ? moveDir.normalized : transform.forward;
    }

    void StartRoll(Vector3 moveDir)
    {
        isRolling = true;
        rollTimer = rollDuration;
        rollCooldownTimer = rollCooldown;

        abilityDirection = moveDir.sqrMagnitude > 0.01f ? moveDir.normalized : transform.forward;
    }
}
