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


    void Update()
    {
        if(!isStasised)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isStasised)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                //TO-DO Kill Player;
            }
            Destroy(this.gameObject);
        }
    }

    //stasis Implementation;

    public void BeginStasis() => isStasised = true;
    public void EndStasis() => isStasised = false;

    public void OnStasisTargeted() => rend.material = highlightMat;
    public void OnStasisUntargeted() => rend.material = normalMat;
}
