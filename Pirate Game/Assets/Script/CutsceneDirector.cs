using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CutsceneDirector : MonoBehaviour
{
    public enum EaseMode
    {
        Linear,
        EaseInOut,
        EaseIn,
        EaseOut,
        EaseInOutCubic
    }

    [Header("Camera")]
    [Tooltip("Leave empty to use Camera.main.")]
    public Camera cutsceneCamera;

    [Header("Shots")]
    public CutsceneShot[] shots;

    [Header("Next Scene")]
    [Tooltip("Exact scene name as listed in File > Build Settings.")]
    public string nextSceneName;

    [Tooltip("Optional pause after the last shot before loading the next scene.")]
    public float delayBeforeLoad = 0f;

    [Header("Global Events")]
    public UnityEvent onCutsceneStart;
    public UnityEvent onCutsceneEnd;

    private void Awake()
    {
        if (cutsceneCamera == null)
            cutsceneCamera = Camera.main;
    }

    private void Start()
    {
        StartCoroutine(PlayCutscene());
    }

    private IEnumerator PlayCutscene()
    {
        if (shots == null || shots.Length == 0)
        {
            Debug.LogWarning("[CutsceneDirector] No shots assigned.");
            LoadNextScene();
            yield break;
        }

        onCutsceneStart?.Invoke();

        foreach (var shot in shots)
            yield return StartCoroutine(PlayShot(shot));

        onCutsceneEnd?.Invoke();

        if (delayBeforeLoad > 0f)
            yield return new WaitForSeconds(delayBeforeLoad);

        LoadNextScene();
    }

    private IEnumerator PlayShot(CutsceneShot shot)
    {
        if (shot.waypoints == null || shot.waypoints.Length == 0)
        {
            Debug.LogWarning("[CutsceneDirector] A shot has no waypoints — skipping.");
            yield break;
        }

        if (shot.waypoints[0].target != null)
            cutsceneCamera.transform.SetPositionAndRotation(
                shot.waypoints[0].target.position,
                shot.waypoints[0].target.rotation
            );

        shot.onShotStart?.Invoke();

        for (int i = 1; i < shot.waypoints.Length; i++)
        {
            CutsceneWaypoint to = shot.waypoints[i];

            if (to.target == null)
            {
                Debug.LogWarning($"[CutsceneDirector] Waypoint {i} has no target — skipping.");
                continue;
            }

            yield return StartCoroutine(MoveToWaypoint(to));
        }

        shot.onShotEnd?.Invoke();
    }

    private IEnumerator MoveToWaypoint(CutsceneWaypoint to)
    {
        float elapsed = 0f;
        float duration = Mathf.Max(to.duration, 0.001f);

        Vector3 startPos = cutsceneCamera.transform.position;
        Quaternion startRot = cutsceneCamera.transform.rotation;

        while (elapsed < duration)
        {
            float t = Ease(elapsed / duration, to.easeMode);

            cutsceneCamera.transform.position = Vector3.Lerp(startPos, to.target.position, t);
            cutsceneCamera.transform.rotation = Quaternion.Slerp(startRot, to.target.rotation, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cutsceneCamera.transform.SetPositionAndRotation(
            to.target.position,
            to.target.rotation
        );
    }

    private void LoadNextScene()
    {
        if (string.IsNullOrEmpty(nextSceneName))
        {
            Debug.LogWarning("[CutsceneDirector] nextSceneName is empty — nowhere to go.");
            return;
        }

        SceneManager.LoadScene(nextSceneName);
    }

    private static float Ease(float t, EaseMode mode)
    {
        switch (mode)
        {
            case EaseMode.Linear: return t;
            case EaseMode.EaseInOut: return t * t * (3f - 2f * t);
            case EaseMode.EaseIn: return t * t;
            case EaseMode.EaseOut: return t * (2f - t);
            case EaseMode.EaseInOutCubic:
                return t < 0.5f
                                               ? 4f * t * t * t
                                               : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;
            default: return t;
        }
    }
}