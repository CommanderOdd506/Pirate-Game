using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StasisRigidbody : MonoBehaviour,IStasisable
{
    [SerializeField] Renderer rend;
    [SerializeField] Material normalMat;
    [SerializeField] Material highlightMat;
    private Rigidbody rb;
    private bool isStasised = false;

    public event System.Action<IStasisable> OnDestroyed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (isStasised) return;

        if (collision.gameObject.CompareTag("Player"))
            CheckpointManager.Instance.RespawnPlayer();

    }
    public void BeginStasis()
    {
        rb.isKinematic = true;
        isStasised = true;
    } 
    public void EndStasis()
    {
        rb.isKinematic = false;
        isStasised = false;  
    }
    public void OnStasisTargeted() => rend.material = highlightMat;
    public void OnStasisUntargeted() => rend.material = normalMat;
}
