using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public string ActualLevelName { get; private set; }
    public List<string> LevelList { get; private set; }
    public ObjectCollider2D ObjectCollider2D { get; private set; }

    void Start()
    {
        LevelList = GetLevelList();
        ActualLevelName = GetActualLevelName();
        ObjectCollider2D = GameObject.Find("ObjectCollider2D").GetComponent<ObjectCollider2D>();
    }

    void LateUpdate()
    {
        if (PlayerDoesNotExist())
        {
            SceneManager.LoadScene(ActualLevelName);
        }
    }

    private void Update()
    {
        if (ObjectCollider2D.IsCollide) ChangeToNextLevel();
    }

    private bool PlayerDoesNotExist()
    {
        return GameObject.Find("Player") == null;
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

    private void ChangeToNextLevel()
    {
        int nextLevelIndex = NextLevelIndex();
        SceneManager.LoadScene(GetSceneName(nextLevelIndex));
    }

    private string GetSceneName(int index)
    {
        if (index == -1) return "MainMenu";
        return LevelList[index];
    }

    private int NextLevelIndex()
    {
        int index = ActualLevelIndex() + 1;
        if (index >= LevelList.Count) return -1;
        return index;
    }

    private int ActualLevelIndex()
    {
        for (int i = 0; i < LevelList.Count; i++)
        {
            if (LevelList[i] == ActualLevelName) return i;
        }
        return 0;
    }

}
