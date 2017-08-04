using UnityEngine;
using UnityEngine.UI;

public class RocksLabel : MonoBehaviour {

    private Text text;

	void Start ()
    {
        text = GetComponent<Text>();
        text.text = "Rocks: " + DataContainer.Instance.Rocks.ToString();
	}
}
