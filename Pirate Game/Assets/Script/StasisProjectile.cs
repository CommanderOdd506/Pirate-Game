using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisProjectile : MonoBehaviour, IStasisable
{
    [SerializeField] private float speed;
    [SerializeField] Renderer rend;
    [SerializeField] Material normalMat;
    [SerializeField] Material highlightMat;

    private bool isStasised = false;

    public event System.Action<IStasisable> OnDestroyed;

    void Update()
    {
        if (!isStasised)
            transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isStasised) return;

        if (collision.gameObject.CompareTag("Player"))
            CheckpointManager.Instance.RespawnPlayer();

        Destroy(gameObject);
    }

    void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    public void BeginStasis() => isStasised = true;
    public void EndStasis() => isStasised = false;
    public void OnStasisTargeted() => rend.material = highlightMat;
    public void OnStasisUntargeted() => rend.material = normalMat;
}