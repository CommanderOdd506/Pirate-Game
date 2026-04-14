using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;
    private PlayerMovement movement;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        // Movement blend
        animator.SetFloat("Speed", movement.CurrentHorizontalSpeed);

        // Ground / air
        animator.SetBool("IsGrounded", movement.IsGrounded);

        // States
        animator.SetBool("IsSprinting", movement.IsSprinting);
        animator.SetBool("IsDashing", movement.IsDashing);
        animator.SetBool("IsRolling", movement.IsRolling);

        // Vertical velocity for jump/fall blend tree
        animator.SetFloat("VerticalVelocity", movement.VerticalVelocity);
    }
}