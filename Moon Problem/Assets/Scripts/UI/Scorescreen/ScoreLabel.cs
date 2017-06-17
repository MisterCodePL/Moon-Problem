using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreLabel : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        DataContainer data = GameObject.Find("DataContainer").GetComponent<DataContainer>();
        float seconds = data.timeContainer.AllSeconds;
        int rocks = data.Rocks;
        int score = (int)Math.Ceiling((rocks / seconds) * 100);
        text.text = "Score: " + score.ToString();
    }
}

