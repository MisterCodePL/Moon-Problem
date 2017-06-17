using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocksCounter : MonoBehaviour
{
    public int Rocks { get; private set; }
    private Text _text;

    private void Start()
    {
        Rocks = 0;
        _text = GetComponentInChildren<Text>();
    }

    public void AddRock()
    {
        Rocks++;
        _text.text = Rocks.ToString();
    }


}
