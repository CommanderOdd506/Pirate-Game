using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStasisManager : MonoBehaviour
{
    [SerializeField] LayerMask stasisLayer;
    [SerializeField] float sphereCastRange;
    [SerializeField] float stasisTimer = 5;
    [SerializeField] private float stasisCooldownTimer = 1.5f;

    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private GameObject standardPost;
    [SerializeField] private GameObject stasisPost;

    [SerializeField] private Image stasisReticle;

    IStasisable bestTarget = null;
    IStasisable previousTarget = null;

    private Camera cam;
    IStasisable stasisObject = null;

    private bool stasisRecharged = true;

    IStasisable stasisedObject = null;
    Coroutine stasisCoroutine;

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        playerInput = GetComponent<PlayerInput>();
        stasisReticle.gameObject.SetActive(false);
    }

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
            StartStasis();

        if (!playerInput.stasisSearchPressed && previousTarget != null)
        {
            previousTarget.OnStasisUntargeted();
            previousTarget = null;
            bestTarget = null;
        }
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
                previousTarget.OnDestroyed -= HandleTargetDestroyed;
            }
            if (bestTarget != null)
            {
                bestTarget.OnStasisTargeted();
                bestTarget.OnDestroyed += HandleTargetDestroyed;
            }
            previousTarget = bestTarget;
        }
    }

    void StartStasis()
    {
        if (!stasisRecharged) return;

        // Only end stasis on the previous object if it's a DIFFERENT target
        if (stasisedObject != null && stasisedObject != bestTarget)
        {
            stasisedObject.EndStasis();
            stasisedObject.OnDestroyed -= HandleStasisedDestroyed;
            stasisedObject = null;
        }

        if (stasisCoroutine != null)
            StopCoroutine(stasisCoroutine);

        SFXManager.instance.AudioPlayOneShot("Stasis");
        stasisRecharged = false;
        StartCoroutine(StasisCooldown());

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
        if (stasisCoroutine != null)
        {
            StopCoroutine(stasisCoroutine);
            stasisCoroutine = null;
        }
        stasisedObject = null;
    }

    private IEnumerator StasisCooldown()
    {
        yield return new WaitForSeconds(stasisCooldownTimer);
        stasisRecharged = true;
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