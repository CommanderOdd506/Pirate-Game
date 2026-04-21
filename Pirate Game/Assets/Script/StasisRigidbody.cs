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

    
    public event System.Action<IStasisable> OnDestroyed;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    public void BeginStasis()
    {
        rb.isKinematic = true;
    } 
    public void EndStasis()
    {
        rb.isKinematic = false;
    }
    public void OnStasisTargeted() => rend.material = highlightMat;
    public void OnStasisUntargeted() => rend.material = normalMat;
}
