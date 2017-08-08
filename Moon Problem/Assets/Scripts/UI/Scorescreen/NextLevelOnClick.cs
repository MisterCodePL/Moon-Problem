using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelOnClick : MonoBehaviour {

	public void OnClick()
    {
        List<string>  levelList = new List<string>
        {
            "Level1",
            "Level2"
        };
        string actualLevel = GameObject.Find("DataContainer").GetComponent<DataContainer>().LevelName;
        ChangeToNextLevel(levelList, actualLevel);
    }

    private int ActualLevelIndex(List<string> LevelList, string ActualLevelName)
    {
        for (int i = 0; i < LevelList.Count; i++)
        {
            if (LevelList[i] == ActualLevelName) return i;
        }
        return 0;
    }

    private void ChangeToNextLevel(List<string> LevelList, string ActualLevelName)
    {
        int nextLevelIndex = NextLevelIndex(LevelList, ActualLevelName);
        SceneManager.LoadScene(GetSceneName(nextLevelIndex, LevelList));
    }

    private string GetSceneName(int index, List<string> LevelList)
    {
        if (index == -1) return "MainMenu";
        return LevelList[index];
    }

    private int NextLevelIndex(List<string> LevelList, string ActualLevelName)
    {
        int index = ActualLevelIndex(LevelList, ActualLevelName) + 1;
        if (index >= LevelList.Count) return -1;
        return index;
    }
}
