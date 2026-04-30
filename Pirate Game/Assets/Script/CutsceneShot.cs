using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class CutsceneShot
{
    [Header("Waypoints")]
    [Tooltip("waypoints[0] is the instant teleport start. Each subsequent waypoint has its own travel duration and easing.")]
    public CutsceneWaypoint[] waypoints;

    [Header("Events")]
    public UnityEvent onShotStart;
    public UnityEvent onShotEnd;
}
[System.Serializable]
public class CutsceneWaypoint
{
    public Transform target;

    [Tooltip("How long it takes to travel TO this waypoint. Ignored for waypoints[0] (the teleport start).")]
    public float duration = 1f;

    [Tooltip("How the camera accelerates across this move.")]
    public CutsceneDirector.EaseMode easeMode = CutsceneDirector.EaseMode.Linear;
}

