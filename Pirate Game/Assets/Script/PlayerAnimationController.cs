using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    private PlayerMovement movement;

    void Awake()
    {
        movement = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        UpdateAnimator();
    }
    private void OnEnable()
    {
        PlayerMovement.OnJump += HandleJumpAnimation;
        CheckpointManager.OnPlayerDie += HandleDieAnimation;
        CheckpointManager.OnPlayerRespawn += HandleRespawnAnimation;
    }

    private void OnDisable()
    {
        PlayerMovement.OnJump -= HandleJumpAnimation;
        CheckpointManager.OnPlayerDie -= HandleDieAnimation;
        CheckpointManager.OnPlayerRespawn -= HandleRespawnAnimation;
    }

    void HandleJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }

    void HandleDieAnimation()
    {
        animator.SetTrigger("Die");
    }

    void HandleRespawnAnimation()
    {
        animator.SetTrigger("Respawn");
    }

    void UpdateAnimator()
    {
        // Movement blend
        animator.SetBool("IsWalking", movement.IsMoving);

        // Ground / air
        animator.SetBool("IsGrounded", movement.IsGrounded);
        animator.SetBool("IsRolling", movement.IsRolling);
        animator.SetBool("IsDashing", movement.IsDashing);
        // Vertical velocity for jump/fall blend tree
        //animator.SetFloat("VerticalVelocity", movement.VerticalVelocity);
    }
}