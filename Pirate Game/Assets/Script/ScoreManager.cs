using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    float currentTime;
    [SerializeField] TextMeshProUGUI timerText;
    // Start is called before the first frame update
    int score;
    int minutes;
    int seconds;


    public int CalculateScore()
    {
        int newScore = 0;
        newScore += CollectibleSystem.Instance.Get("gold");
        return 0;
    }
    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
