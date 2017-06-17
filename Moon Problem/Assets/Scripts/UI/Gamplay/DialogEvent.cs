using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang.Runtime.DynamicDispatching;
using UnityEngine;
using UnityEngine.UI;

public class DialogEvent : MonoBehaviour
{
    [TextArea]
    public string Text;

    public Text Label;
    public float DisplayTime;

    private float _x;
    private float _playerX;
    private float _actualTime;

    void Start()
    {
        _actualTime = 0;
    }

    void Update()
    {
        if(IsDisplay()) _actualTime += Time.fixedDeltaTime;
        GetActualPositions();
        if (InRange() && IsNotExceededTime() && !IsDisplay())
        {
            Label.text = Text;
        }
        if (!IsNotExceededTime())
        {
            Label.text="";
            Destroy(gameObject.GetComponent<DialogEvent>());
        }
    }

    private bool IsDisplay()
    {
        return Label.text == Text;
    }

    private void GetActualPositions()
    {
        _x = GetComponent<Transform>().position.x;
        _playerX = GameObject.Find("Player").transform.position.x;
    }

    private bool InRange()
    {
        return Mathf.Abs(_playerX - _x) < 1f;
    }

    private bool IsNotExceededTime()
    {
        return _actualTime < DisplayTime;
    }
}
