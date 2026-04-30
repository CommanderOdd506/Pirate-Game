using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Cinemachine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    float currentTime;

    public CinemachineFreeLook freeLookCam;
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;

    public GameObject resumeButton;

    [SerializeField] TextMeshProUGUI timerText;

    public Animator playerAnimator;

    [Header("UI")]

    public GameObject completionPanel;
    [SerializeField] TextMeshProUGUI timerTextEndScreen;
    [SerializeField] TextMeshProUGUI coinsTextEndScreen;
    [SerializeField] TextMeshProUGUI deathsTextEndScreen;
    [SerializeField] TextMeshProUGUI totalScoreTextEndScreen;

    public bool hasWon = false;
    // Start is called before the first frame update
    int score;
    int minutes;
    int seconds;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    public int CalculateScore()
    {
        int newScore = 0;
        newScore += CollectibleSystem.Instance.Get("gold");
        return newScore;
    }

    public void CompleteLevel()
    {
        hasWon = true;
        ShowEndScreenUI();
        PlayerWinEffects();
    }

    void PlayerWinEffects()
    {
         playerAnimator.SetTrigger("Win");
         playerInput.enabled = false;
         playerMovement.enabled = false;
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
         freeLookCam.gameObject.SetActive(false);
    }

    void ShowEndScreenUI()
    {
        completionPanel.SetActive(true);
        timerText.gameObject.SetActive(false);
        coinsTextEndScreen.text = CollectibleSystem.Instance.Get("gold").ToString();
        timerTextEndScreen.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        deathsTextEndScreen.text = CheckpointManager.Instance.Deaths.ToString();
        totalScoreTextEndScreen.text = CalculateScore().ToString();
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }
    // Update is called once per frame
    void Update()
    {
        if (hasWon) return;
        currentTime += Time.deltaTime;

        minutes = Mathf.FloorToInt(currentTime / 60);
        seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
