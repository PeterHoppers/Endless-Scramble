using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class TimeTracking : MonoBehaviour
{
	Text _text;
    Transform _panel;
    GameObject levelMenu;

    double goldTime;
    double silverTime;
    double bronzeTime;

    Transform goldMedal;
    Transform silverMedal;
    Transform bronzeMedal;

	float totalTime;
	float delayTimer;

	bool endTime;
	bool playTime = true;

    // Use this for initialization
    void Start()
    {
        //Find different gameObjects and transforms and check if they exist
        levelMenu = GameObject.FindGameObjectWithTag("LevelMenu");

        _panel = levelMenu.transform.Find("ResultsPanel");

        if (_panel == null)
            Debug.LogError("Eiter you changed the location of Results Panel or you changed the name.");

        _text = levelMenu.transform.Find("TimeText").GetComponent<Text>();

        if (_text == null)
            Debug.LogError("Eiter you changed the location of Time Text or you changed the name.");

        //finds the transformed named the name of the medal in the panel and turns it off
        bronzeMedal = _panel.Find("Bronze");

        if (bronzeMedal != null)
            bronzeMedal.gameObject.SetActive(false);

        silverMedal = _panel.Find("Silver");

        if (silverMedal != null)
            silverMedal.gameObject.SetActive(false);

        goldMedal = _panel.transform.Find("Gold");

        if (goldMedal != null)
            goldMedal.gameObject.SetActive(false);

        _panel.gameObject.SetActive(false);

        DeathManager.PlayerKilled += ResetTimer; //subscribes resetTimer to whenever the player dies
        
        Time.timeScale = 1;

        //Added loading times from an XML files
        GoalCollector goalTimes = GoalCollector.Load(Path.Combine(Application.dataPath, "Resources/XML/goalTimes.xml")); //Loads XML File. Code below. 

        int currentSceneNum = SceneManager.GetActiveScene().buildIndex;
        goldTime = goalTimes.GetGoldTime(currentSceneNum);
        silverTime = goalTimes.GetSilverTime(currentSceneNum);
        bronzeTime = goalTimes.GetBronzeTime(currentSceneNum);
    }

    // Update is called once per frame
    void Update () 
	{
		if (!endTime) //if the level has not ended yet
		{
			if (playTime) //if the game isn't paused
			{
                UpdateTimer();
			}
		}
    }

    //Using the time since the level began and the time calculated from it being reset or paused
    //the time since the player has been playing the level can be caluculated and displayed
    void UpdateTimer()
    {
        totalTime = (Time.timeSinceLevelLoad - delayTimer);
        totalTime = Mathf.Round(totalTime * 100f) / 100f;
        _text.text = totalTime.ToString();
    }

    //Resets Timer back to 0 on screen by making the delayTimer value equal 
    //how long the player has been playing the level so far
	public void ResetTimer()
	{
		delayTimer = Time.timeSinceLevelLoad;
	}
	
    //Pauses or Resumes the timer, based upon the parameters given
	public void PauseTimer(bool isActive)
	{
		playTime = isActive;
		
		if (isActive)
			delayTimer = (Time.timeSinceLevelLoad - totalTime);		
	}

    public void FinishedLevel()
    {
        _text.text = "Final Time Is: " + totalTime.ToString();
        _panel.gameObject.SetActive(true);
        _panel.transform.Find("BronzeText").GetComponent<Text>().text = bronzeTime.ToString("0.##");
        _panel.transform.Find("SilverText").GetComponent<Text>().text = silverTime.ToString("0.##");
        _panel.transform.Find("GoldText").GetComponent<Text>().text = goldTime.ToString("0.##");

        endTime = true; //stop the timer

        int medalNum = 0;

        medalNum = MedalHandler.CalcMedalType(bronzeTime, silverTime, goldTime, totalTime);

        switch (medalNum) //activates a medal based upon the number it gets
        {
            case 3:
                goldMedal.gameObject.SetActive(true);
                goto case 2;
            case 2:
                silverMedal.gameObject.SetActive(true);
                goto case 1;
            case 1:
                bronzeMedal.gameObject.SetActive(true);
                break;
            default:
                break;
        }

        Time.timeScale = 0;
    }
}
