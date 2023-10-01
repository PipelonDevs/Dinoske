using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timerDuration = 600f;
    public TextMeshProUGUI timerText;

    public float timeRemaining;

    void Awake()
    {
        timeRemaining = 0;
    }

    void Update()
    {
        if (timeRemaining < timerDuration)
        {
            timeRemaining += Time.deltaTime;
            int minutes = (int)(timeRemaining / 60);
            int seconds = (int)(timeRemaining % 60);
            int hundredths = (int)((timeRemaining * 100) % 100);

            timerText.text = $"{minutes:00}:{seconds:00}";

        }
        else
        {
            timerText.text = "Timer finished";
        }
    }
}
