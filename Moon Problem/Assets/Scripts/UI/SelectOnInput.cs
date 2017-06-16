using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem EventSystem;
    public GameObject SelectedObject;

    private bool _buttonSelected;

    void Update ()
    {
        if (Input.GetAxis("Vertical") != 0 && _buttonSelected==false)
        {
            EventSystem.SetSelectedGameObject(SelectedObject);
            _buttonSelected = true;
        }
	}

    private void OnDisable()
    {
        _buttonSelected = false;
    }
}
