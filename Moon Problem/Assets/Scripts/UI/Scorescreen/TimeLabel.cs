using UnityEngine;
using UnityEngine.UI;

public class TimeLabel : MonoBehaviour
{
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        TimeContainer time = GameObject.Find("DataContainer").GetComponent<DataContainer>().timeContainer;
        text.text = "Time: " + time.Minutes+":"+time.Seconds;
    }
}
