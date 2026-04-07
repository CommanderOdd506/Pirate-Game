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

    int nextPoint = 0;
    Vector3 startPosition;

    public Vector3 velocity {get; private set; }

    public bool isStasised = false;
    // Start is called before the first frame update
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

    Vector3 currentPoint { 
        get { 
            if(points == null || points.Length == 0)
            {
                return transform.position;
            }

            return points[nextPoint] + startPosition;
        } 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isStasised) return;

        var newPosition = Vector3.MoveTowards(transform.position, currentPoint, speed * Time.deltaTime);

        if (Vector3.Distance(newPosition, currentPoint) < 0.001)
        {
            newPosition = currentPoint;

            nextPoint += 1;
            nextPoint %= points.Length;

            
        }

        velocity = (newPosition - transform.position) / Time.deltaTime;

        transform.position = newPosition;
    }

    //stasis implementation

    public void BeginStasis()
    {
        isStasised = true;
    }

    public void EndStasis()
    {
        isStasised = false;
    }

    public void OnStasisTargeted()
    {
        rend.material = highlightMat;
    }

    public void OnStasisUntargeted()
    {
        rend.material = normalMat;
    }
}
