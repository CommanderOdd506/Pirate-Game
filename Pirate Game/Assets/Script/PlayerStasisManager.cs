using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStasisManager : MonoBehaviour
{
    [SerializeField] LayerMask stasisLayer;
    [SerializeField] float sphereCastRange;

    IStasisable bestTarget = null;
    private Camera cam;

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //use find stasis target if input 
        //then use otheer input to activate stasis 
    }

    void FindStasisTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, sphereCastRange, stasisLayer);

        float bestScore = 0.7f;
        foreach (Collider col in hits)
        {
            IStasisable stasisObject = col.GetComponent<IStasisable>();

            if (stasisObject == null) continue;

            Vector3 direction = (transform.position - col.transform.position).normalized;

            float dot = Vector3.Dot(direction, col.transform.position);

            if (dot < bestScore)
                bestScore = dot;
            bestTarget = stasisObject;
            Debug.Log("New stasis item selected" + col.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

       Gizmos.DrawWireSphere(transform.position, sphereCastRange);
    }
}
