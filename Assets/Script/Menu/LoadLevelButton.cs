using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadLevelButton : MonoBehaviour {

    public int levelSelect;

    Image image;

    public void LevelSelect()
    {
        if (!GlobalVars.isTutorial)
        {
            GoalTimes.nextLevel = levelSelect;
            SceneManager.LoadScene(levelSelect);
        }
    }

    private void Start()
    {
        image = GetComponent<Image>();
        SetColorToMedal();
    }

    void SetColorToMedal()
    {
        int medalType = MedalHandler.GetMedalType(levelSelect);

        switch (medalType)
        {
            case 3:
                image.color = new Color32(255, 215, 0, 255);
                break;
            case 2:
                image.color = Color.gray;
                break;
            case 1:
                image.color = new Color32(47, 31, 15, 255);
                break;
        }
    }
}
