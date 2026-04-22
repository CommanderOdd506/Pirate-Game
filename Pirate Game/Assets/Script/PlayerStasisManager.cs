using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStasisManager : MonoBehaviour
{
    [SerializeField] LayerMask stasisLayer;
    [SerializeField] float sphereCastRange;
    [SerializeField] float stasisTimer = 5;

    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private GameObject standardPost;
    [SerializeField] private GameObject stasisPost;

    [SerializeField] private Image stasisReticle;

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
        stasisReticle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.stasisSearchPressed && !PauseMenu.Instance.IsPaused)
        {
            FindStasisTarget();
            SetPostProcessing(true);
            stasisReticle.gameObject.SetActive(true);
        }
        else
        {
            SetPostProcessing(false);
            stasisReticle.gameObject.SetActive(false);
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
            }
        }

        if (bestTarget != previousTarget)
        {
            if (previousTarget != null)
            {
                previousTarget.OnStasisUntargeted();
                previousTarget.OnDestroyed -= HandleTargetDestroyed; // unsubscribe old
            }
            if (bestTarget != null)
            {
                bestTarget.OnStasisTargeted();
                bestTarget.OnDestroyed += HandleTargetDestroyed; // subscribe new
            }
            previousTarget = bestTarget;
        }
    }

    void StartStasis()
    {
        if (stasisedObject != null)
        {
            stasisedObject.EndStasis();
            stasisedObject.OnDestroyed -= HandleStasisedDestroyed;
        }

        if (stasisCoroutine != null)
            StopCoroutine(stasisCoroutine);

        bestTarget.BeginStasis();
        stasisedObject = bestTarget;
        stasisedObject.OnDestroyed += HandleStasisedDestroyed;
        stasisCoroutine = StartCoroutine(StasisTimer());
    }

    void StopStasis()
    {
        if (stasisedObject != null)
        {
            stasisedObject.EndStasis();
            stasisedObject.OnDestroyed -= HandleStasisedDestroyed;
            stasisedObject = null;
        }
        stasisCoroutine = null;
    }

    private void HandleTargetDestroyed(IStasisable destroyed)
    {
        destroyed.OnDestroyed -= HandleTargetDestroyed;

        if (bestTarget == destroyed) bestTarget = null;
        if (previousTarget == destroyed) previousTarget = null;
    }

    private void HandleStasisedDestroyed(IStasisable destroyed)
    {
        destroyed.OnDestroyed -= HandleStasisedDestroyed;

        // Object is already gone so skip EndStasis(), just clean up our side
        if (stasisCoroutine != null)
        {
            StopCoroutine(stasisCoroutine);
            stasisCoroutine = null;
        }
        stasisedObject = null;
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
