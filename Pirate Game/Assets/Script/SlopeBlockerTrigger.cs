using UnityEngine;

public class SlopeBlockerTrigger : MonoBehaviour
{
    [Header("The direction this trigger pushes the player (local space)")]
    public Vector3 pushDirection;
    public float pushForce = 8f;
    public LayerMask terrainLayer;

    private PlayerMovement player;
    private bool isTouching;

    void Start()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    void OnTriggerStay(Collider other)
    {
        if ((terrainLayer.value & (1 << other.gameObject.layer)) == 0) return;
        isTouching = true;
    }

    void OnTriggerExit(Collider other)
    {
        if ((terrainLayer.value & (1 << other.gameObject.layer)) == 0) return;
        isTouching = false;
    }

    // Feed the push once per frame from Update so it always lands in the same
    // phase as PlayerMovement.Update — no more ordering races with OnTriggerStay.
    // No Time.deltaTime here; PlayerMovement applies it once when calling Move.
    void Update()
    {
        if (!isTouching) return;

        Vector3 worldPush = transform.parent.TransformDirection(pushDirection).normalized;
        player.AddSlopePush(worldPush * pushForce);
    }
}