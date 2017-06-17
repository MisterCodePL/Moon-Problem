using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Judge : MonoBehaviour
{
    public ObjectCollider2D ObjectCollider2D { get; private set; }

    void Start()
    {
        ObjectCollider2D = GameObject.Find("ObjectCollider2D").GetComponent<ObjectCollider2D>();
    }

    void LateUpdate()
    {
        if (PlayerDoesNotExist())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void Update()
    {
        if (ObjectCollider2D.IsCollide) GoToScoreScreen();
    }

    private bool PlayerDoesNotExist()
    {
        return GameObject.Find("Player") == null;
    }

    private void GoToScoreScreen()
    {
        SceneManager.LoadScene("Scorescreen");
    }

}
