using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer instance;

    [SerializeField] TextMeshProUGUI timerText;
    public float startTime = 200f;
    public float timeLeft;

    public bool timeStopped;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartTime();
        timeStopped = false;
    }

    private void Update()
    {

        if (timeLeft <= 0)
        {

            // Game Over
            timeLeft = 0;
            timerText.color = Color.red;
            SceneManager.LoadScene("GameOverScene");

        }
        
        else if (timeStopped == false)
        {
            timeLeft -= Time.deltaTime;
        }
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartTime()
    {
        timeLeft = startTime;
    }

    public void AddTimer(int time)
    {
        timeLeft += time;
    }
}
