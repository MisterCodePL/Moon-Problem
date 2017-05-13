using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string ActualLevelName { get; private set; }
    public List<string> LevelList { get; private set; }

    void Start()
    {
        LevelList = GetLevelList();
        ActualLevelName = GetActualLevelName();
    }

    void LateUpdate()
    {
        var player = FindPlayer();
        if (IsDeath(player))
        {
            SceneManager.LoadScene(ActualLevelName);
        }
    }

    private List<string> GetLevelList()
    {
        var levelList = new List<string>
        {
            "Demo"
        };
        return levelList;
    }

    private string GetActualLevelName()
    {
        var scene = SceneManager.GetActiveScene();
        var levelName = scene.name;
        return levelName;
    }

    private GameObject FindPlayer()
    {
        return GameObject.Find("Player");
    }

    private bool IsDeath(GameObject player)
    {
        if (player == null) return true;
        var playerTransform = player.transform;
        var playerPosition = playerTransform.position;
        return playerPosition.y < -7.5f;
    }

}
