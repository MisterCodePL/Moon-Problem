using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        DestroyDeathGameObject();

    }

    private void DestroyDeathGameObject()
    {
        var deathGameObjects = FindAllDeathGameObjects();
        foreach (var o in deathGameObjects)
        {
            Destroy(o);
        }
    }

    private List<string> GetLevelList()
    {
        var levelList = new List<string>
        {
            "Demo",
            "Level1"
        };
        return levelList;
    }

    private string GetActualLevelName()
    {
        var scene = SceneManager.GetActiveScene();
        var levelName = scene.name;
        return levelName;
    }

    private List<GameObject> FindAllDeathGameObjects()
    {
        var list = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        list.Add(GameObject.Find("Player"));
        list = list.Select(x => x)
            .Where(t => t.transform.position.y < -7.5f)
            .ToList();
        return list;
    }

}
