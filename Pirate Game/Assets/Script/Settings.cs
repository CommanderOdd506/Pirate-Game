using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Cinemachine;

public class Settings : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer mainAudioMixer;
    public string masterChannel = "MasterVol";
    public Slider vol;

    [Header("Display")]
    public Toggle fullscreenToggle;

    [Header("Sensitivity")]
    public Slider sensSlider;
    public CinemachineFreeLook freeLookCamera;

    // Base speeds you set in the Inspector on your FreeLook cam
    [SerializeField] private float baseXSpeed = 300f;
    [SerializeField] private float baseYSpeed = 2f;

    void Start()
    {
        ChangeMouseSensitivity();
    }

    void OnEnable()
    {
        LoadUISettings();
    }

    // --- Volume ---

    public void ChangeVolume()
    {
        float db = vol.value < 0.02f ? -80f : Mathf.Log10(vol.value) * 20f;
        mainAudioMixer.SetFloat(masterChannel, db);
    }

    // --- Sensitivity ---

    public void ChangeMouseSensitivity()
    {
        float sens = sensSlider.value; // expected range: 0.0 – 1.0

        if (freeLookCamera != null)
        {
            freeLookCamera.m_XAxis.m_MaxSpeed = baseXSpeed * sens;
            freeLookCamera.m_YAxis.m_MaxSpeed = baseYSpeed * sens;
        }

        PlayerPrefs.SetFloat("Sensitivity", sens);
        PlayerPrefs.Save();
    }

    // --- Fullscreen ---

    public void ChangeFullscreen()
    {
        bool isFullscreen = fullscreenToggle.isOn;
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
        PlayerPrefs.Save();
    }

    // --- Load saved prefs into UI ---

    public void LoadUISettings()
    {
        // Volume
        if (mainAudioMixer.GetFloat(masterChannel, out float masterDb))
            vol.value = Mathf.Pow(10f, masterDb / 20f);

        // Sensitivity
        float savedSens = PlayerPrefs.GetFloat("Sensitivity", 0.3f);
        sensSlider.value = savedSens;
        ChangeMouseSensitivity(); // apply immediately on load

        // Fullscreen
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.SetIsOnWithoutNotify(isFullscreen);
        Screen.fullScreen = isFullscreen;
    }
}