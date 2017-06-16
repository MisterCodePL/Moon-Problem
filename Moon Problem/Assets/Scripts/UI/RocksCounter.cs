using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocksCounter : MonoBehaviour
{
    private int _rocks;
    private Text _text;

    private void Start()
    {
        _rocks = 0;
        _text = GetComponentInChildren<Text>();
    }

    public void AddRock()
    {
        _rocks++;
        _text.text = _rocks.ToString();
    }


}
