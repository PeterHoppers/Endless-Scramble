using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadSceneButton : MonoBehaviour {

    int currentLevelNum;
    private void Start()
    {
        currentLevelNum = SceneManager.GetActiveScene().buildIndex;
    }

    public void GoToMenu()
    {
        if (!GlobalVars.isTutorial)
        {
            Time.timeScale = 1;
            GoalTimes.ChangeScene();
            SceneManager.LoadScene("mainMenu");
        }
    }

    public void Retry()
    {
        GoalTimes.ChangeScene();
        if (!GlobalVars.isTutorial)
            SceneManager.LoadScene(currentLevelNum);
    }

    public void NextLevel()
    {
        if (!GlobalVars.isTutorial)
        {
            GoalTimes.ChangeScene();
            GoalTimes.nextLevel = currentLevelNum + 1;
            SceneManager.LoadScene(GoalTimes.nextLevel);
        }
    }
}
