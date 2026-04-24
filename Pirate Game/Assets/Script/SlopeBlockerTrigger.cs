using UnityEngine;

public class SlopeBlockerTrigger : MonoBehaviour
{
    [Header("the direction this trigger pushes the player (local space)")]
    public Vector3 pushDirection;
    public float pushForce = 8f;
    public LayerMask terrainLayer;

    private PlayerMovement player;

    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    void OnTriggerStay(Collider other)
    {
        if ((terrainLayer.value & (1 << other.gameObject.layer)) == 0) return;

        // convert local push direction to world space so it stays correct as player rotates
        Vector3 worldPush = transform.parent.TransformDirection(pushDirection).normalized;
        player.AddSlopePush(worldPush * pushForce * Time.deltaTime);
    }
}