using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStasisManager : MonoBehaviour
{
    [SerializeField] LayerMask stasisLayer;
    [SerializeField] float sphereCastRange;
    [SerializeField] float stasisTimer = 5;

    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private GameObject standardPost;
    [SerializeField] private GameObject stasisPost;

    IStasisable bestTarget = null;
    IStasisable previousTarget = null;

    private Camera cam;
    IStasisable stasisObject = null;

    IStasisable stasisedObject = null;
    Coroutine stasisCoroutine;

    

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.stasisSearchPressed)
        {
            FindStasisTarget();
            SetPostProcessing(true);
        }
        else
        {
            SetPostProcessing(false);
        }
        if (playerInput.stasisActivatePressed && bestTarget != null)
        {
            StartStasis();
        }

        if (!playerInput.stasisSearchPressed && previousTarget != null)
        {
            previousTarget.OnStasisUntargeted();
            previousTarget = null;
            bestTarget = null;
        }
        //use find stasis target if input 
        //then use otheer input to activate stasis 
    }

    void SetPostProcessing(bool spp)
    {
        if (spp && !stasisPost.activeSelf)
        {
            standardPost.SetActive(false);
            stasisPost.SetActive(true);
        }
        else if (!spp && !standardPost.activeSelf)
        {
            standardPost.SetActive(true);
            stasisPost.SetActive(false);
        }
        return;
    }

    void FindStasisTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, sphereCastRange, stasisLayer);

        bestTarget = null;
        float bestScore = 0.7f;
        foreach (Collider col in hits)
        {
            stasisObject = col.GetComponent<IStasisable>();

            if (stasisObject == null) continue;

            Vector3 directionToTarget = (col.transform.position - cam.transform.position).normalized;

            float dot = Vector3.Dot(cam.transform.forward, directionToTarget);

            if (dot > bestScore)
            {
                bestScore = dot;
                bestTarget = stasisObject;
                Debug.Log("New stasis item selected" + col.name);
            }
            
        }
        if (bestTarget != previousTarget)
        {
            if (previousTarget != null)
                previousTarget.OnStasisUntargeted();

            if (bestTarget != null)
                bestTarget.OnStasisTargeted();

            previousTarget = bestTarget;
        }

    }

    void StartStasis()
    {
        // Stop previous stasis
        if (stasisedObject != null)
        {
            stasisedObject.EndStasis();
        }

        // Stop previous timer
        if (stasisCoroutine != null)
        {
            StopCoroutine(stasisCoroutine);
        }

        bestTarget.BeginStasis();
        stasisedObject = bestTarget;

        stasisCoroutine = StartCoroutine(StasisTimer());
    }

    void StopStasis()
    {
        if (stasisedObject != null)
        {
            stasisedObject.EndStasis();
            stasisedObject = null;
        }

        stasisCoroutine = null;
    }
    private IEnumerator StasisTimer()
    {
        yield return new WaitForSeconds(stasisTimer);
        StopStasis();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

       Gizmos.DrawWireSphere(transform.position, sphereCastRange);
    }
}
