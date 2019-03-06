using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialTime : MonoBehaviour
{
    Text _text;
    Transform _panel;
    GameObject levelMenu;
    DialogueButton dButton;

    public float GoldTime;
    public float SilverTime;
    public float BronzeTime;

    Transform goldMedal;
    Transform silverMedal;
    Transform bronzeMedal;

    public float totalTime;
    float delayTimer;

    bool endTime;
    bool playTime = true;
    bool crashing = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (!endTime) //if the level has not ended yet
        {
            if (playTime) //if the game isn't paused
            {
                UpdateTimer();
            }
        }

        if (crashing) //flag flipped when the game starts to crash
        {
            if (Input.GetKeyDown("e")) //when the player presses the button to move onto the screen that crashes
            {
                StartCoroutine(dButton.Crash());
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

    //When something hits its 2D collider
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            //----------Special Cases only During Tutorial----------------
            dButton = GameObject.FindGameObjectWithTag("ConvoUIButton").GetComponent<DialogueButton>();
            dButton.SwitchingState(); //turns on dialogue when levels is finished


            if (GlobalVars.isTutorial) //if a level is completed before the game "crashes"
            {
                crashing = true; //activate crashing prompt
            }
            //------------------This is normal-----------------
            else //Display the result values
            {
                _text.text = "Final Time Is: " + totalTime.ToString();
                _panel.gameObject.SetActive(true);
                _panel.transform.Find("BronzeText").GetComponent<Text>().text = BronzeTime.ToString() + ".00";
                _panel.transform.Find("SilverText").GetComponent<Text>().text = SilverTime.ToString() + ".00";
                _panel.transform.Find("GoldText").GetComponent<Text>().text = GoldTime.ToString() + ".00";
            }

            endTime = true; //stop the timer

            goldMedal.gameObject.SetActive(true);           
            silverMedal.gameObject.SetActive(true);
            bronzeMedal.gameObject.SetActive(true);            
        }
    }
}
