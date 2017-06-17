using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    private Text _text;
    public TimeContainer time { get; private set; }


    private void Start()
    {
        _text = GetComponentInChildren<Text>();
        time = new TimeContainer(0);
    }

    private void FixedUpdate()
    {
        time.AddSeconds(Time.fixedDeltaTime);
        UpdateDisplayClock();
    }

    private void UpdateDisplayClock()
    {
        int minutes = time.Minutes;
        int seconds = time.Seconds;
        string secondsText = seconds.ToString();
        if (seconds < 10) secondsText = "0" + seconds;
        _text.text = minutes + ":" + secondsText;
    }
}
