using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IStasisable
{
    [SerializeField] float speed;
    [SerializeField] Vector3[] points = { };
    [SerializeField] Renderer rend;
    [SerializeField] Material normalMat;
    [SerializeField] Material highlightMat;
    [SerializeField] Material outlineMat; // your outline shader material

    int nextPoint = 0;
    Vector3 startPosition;
    public Vector3 velocity { get; private set; }
    public bool isStasised = false;
    public event System.Action<IStasisable> OnDestroyed;

    void Start()
    {
        if (points == null || points.Length < 2)
        {
            Debug.LogError("Platform needs atleast 2 points to work");
            return;
        }
        startPosition = transform.position;
        transform.position = currentPoint;
    }

    Vector3 currentPoint
    {
        get
        {
            if (points == null || points.Length == 0)
                return transform.position;
            return points[nextPoint] + startPosition;
        }
    }

    void FixedUpdate()
    {
        if (isStasised)
        {
            velocity = Vector3.zero;
            return;
        }

        Vector3 oldPosition = transform.position;
        var newPosition = Vector3.MoveTowards(transform.position, currentPoint, speed * Time.deltaTime);
        if (Vector3.Distance(newPosition, currentPoint) < 0.001f)
        {
            newPosition = currentPoint;
            nextPoint = (nextPoint + 1) % points.Length;
        }
        transform.position = newPosition;
        velocity = (transform.position - oldPosition) / Time.deltaTime;
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

    // --- IStasisable ---
    public void BeginStasis()
    {
        isStasised = true;
        AddOutline();
    }

    public void EndStasis()
    {
        isStasised = false;
        RemoveOutline();
    }

    // these stay exactly as before
    public void OnStasisTargeted() => rend.material = highlightMat;
    public void OnStasisUntargeted() => rend.material = normalMat;
}