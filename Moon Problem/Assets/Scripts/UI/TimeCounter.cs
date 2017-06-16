using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    private Text _text;
    private float _timeInSeconds;


    private void Start()
    {
        _text = GetComponentInChildren<Text>();
        _timeInSeconds = 0;
    }

    private void FixedUpdate()
    {
        _timeInSeconds += Time.fixedDeltaTime;
        UpdateDisplayClock();
    }

    private void UpdateDisplayClock()
    {
        int minutes = (int)(_timeInSeconds / 60);
        int seconds = (int)_timeInSeconds - minutes * 60;
        string secondsText = seconds.ToString();
        if (seconds < 10) secondsText = "0" + seconds;
        _text.text = minutes + ":" + secondsText;
    }
}
