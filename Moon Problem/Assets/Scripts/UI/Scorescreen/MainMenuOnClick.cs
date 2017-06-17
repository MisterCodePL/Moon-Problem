using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOnClick : MonoBehaviour {

	public void OnClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
