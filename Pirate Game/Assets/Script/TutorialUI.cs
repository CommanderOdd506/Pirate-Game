using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    public static TutorialUI Instance { get; private set; }

    public string[] keyboardStrings;
    public string[] controllerStrings;
    public TextMeshProUGUI tutorialText;

    private static bool _usingController = false;

    private void Awake()
    {
        Instance = this;
        InputSystem.onActionChange += OnActionChange;
    }

    private void OnDestroy()
    {
        InputSystem.onActionChange -= OnActionChange;
    }

    // Fires whenever any input action is performed — we check what device triggered it
    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change != InputActionChange.ActionPerformed) return;

        var action = obj as InputAction;
        var device = action?.activeControl?.device;
        if (device == null) return;

        _usingController = device is Gamepad;
    }

    public void TriggerUI(int index)
    {
        tutorialText.text = _usingController ? controllerStrings[index] : keyboardStrings[index];
    }

    public void CloseUI()
    {
        tutorialText.text = "";
    }

    public static bool IsUsingController() => _usingController;
}