using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StasisProjectile : MonoBehaviour, IStasisable
{
    [SerializeField] private float speed;
    [SerializeField] Renderer rend;
    [SerializeField] Material normalMat;
    [SerializeField] Material highlightMat;
    [SerializeField] Material outlineMat;
    [SerializeField] private ParticleSystem stasisVFX;
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
    void OnDestroy()
    {
        OnDestroyed?.Invoke(this);
    }

    public void BeginStasis()
    {
        isStasised = true;
        stasisVFX?.Play();
        AddOutline();
    }
    public void EndStasis()
    {
        isStasised = false;
        RemoveOutline();
    }
    public void OnStasisTargeted() => rend.material = highlightMat;
    public void OnStasisUntargeted() => rend.material = normalMat;
}