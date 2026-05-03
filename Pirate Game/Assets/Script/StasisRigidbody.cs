using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class StasisRigidbody : MonoBehaviour, IStasisable
{
    [SerializeField] Renderer rend;
    [SerializeField] Material normalMat;
    [SerializeField] Material highlightMat;
    [SerializeField] Material outlineMat;
    private Rigidbody rb;
    private bool isStasised = false;

    public bool IsStasised => isStasised;

    [SerializeField] private ParticleSystem stasisVFX;

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

    void AddOutline()
    {
        var mats = new List<Material>(rend.materials);
        bool alreadyHasOutline = mats.Exists(m => m.name.Contains(outlineMat.name));
        if (!alreadyHasOutline)
        {
            mats.Add(outlineMat);
            rend.materials = mats.ToArray();
        }
    }

    void RemoveOutline()
    {
        var mats = new List<Material>(rend.materials);
        mats.RemoveAll(m => m.name.Contains(outlineMat.name));
        rend.materials = mats.ToArray();
    }

    public void BeginStasis()
    {
        rb.isKinematic = true;
        isStasised = true;
        stasisVFX?.Play();
        AddOutline();
    }
    public void EndStasis()
    {
        rb.isKinematic = false;
        isStasised = false;
        RemoveOutline();
    }
    public void OnStasisTargeted() => rend.material = highlightMat;
    public void OnStasisUntargeted() => rend.material = normalMat;
}
