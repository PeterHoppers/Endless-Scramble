using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadLevelButton : MonoBehaviour {

    public int levelSelect;

    public void LevelSelect()
    {
        if (!GlobalVars.isTutorial)
        {
            GoalTimes.nextLevel = levelSelect;
            SceneManager.LoadScene(levelSelect);
        }
    }
}
