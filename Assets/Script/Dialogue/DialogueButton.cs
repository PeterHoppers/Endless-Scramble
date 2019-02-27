using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

//Activates the dialogue system, either by trigger or start
public class DialogueButton : MonoBehaviour {

    public RawImage image;
    [HideInInspector]
	public ConversationScript convoScriptText;
	
	GameObject goal;
	GameObject player;

    public float waitTime;
	public bool switchState = false;

    void Start() //used onStart of the level. Grabs the components in scene
    {
        convoScriptText = GameObject.FindGameObjectWithTag("ConvoUIText").GetComponent<ConversationScript>();

        if (convoScriptText == null)
            Debug.LogError("UI Text for conversation isn't tagged correctly.");

        player = GameObject.FindGameObjectWithTag("Player");
        goal = GameObject.FindGameObjectWithTag("Goal");

        if (goal == null)
            Debug.LogError("Goal is not tagged correctly.");

        convoScriptText.GetComponent<ConversationScript>().enabled = false; //makes sure the conversation doesn't start on start
        player.GetComponent<Movement>().enabled = true; //makes sure that the player can move

        if (GlobalVars.isTutorial || SceneManager.GetActiveScene().name == "Tutorial") //if it tutorial, bring it up at the beguinning
        {
            SwitchingState();
            this.gameObject.GetComponent<Image>().enabled = false;
            this.gameObject.GetComponent<Button>().enabled = false;
            this.gameObject.GetComponentInChildren<Image>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<Image>().enabled = true;
            this.gameObject.GetComponent<Button>().enabled = true;
            this.gameObject.GetComponentInChildren<Image>().enabled = true;
        }
    }

    //either turns on the dialgoue prompt of turns it off
    public void SwitchingState() 
	{
		switchState = !switchState;
		
		player.GetComponent<Movement>().enabled = !switchState; //changes the state of movement
        this.transform.parent.GetComponent<Animator>().SetBool("isUsed", switchState); //drops down or pulls up the prompt
        
        goal.GetComponent<TimeTracking>().PauseTimer(!switchState); //either stops or starts the timer

        if (switchState)
            StartCoroutine(PauseAnimation()); //starts to write the text
        else
            convoScriptText.enabled = false; //ends the text
    }

    //slight pause in animation before the writing beguins. Otherwise, text starts before animation finishes
    IEnumerator PauseAnimation() 
    {
        yield return new WaitForSeconds(waitTime);
        convoScriptText.enabled = true;
    }

    //Called when you want to crash the game
    public IEnumerator Crash()
    {
        yield return new WaitUntil(convoScriptText.ConvoMatchesIndex); //waits until the index of the conversation matches when it actually crashes
        StartCoroutine(CrashingOccuring());
    }

    IEnumerator CrashingOccuring()
    {
        yield return new WaitForSeconds(.5f); // a slight delay

        //Grab all the information needed for the game to feign crashing and play those animations
        GlobalVars.isCrashing = true;
        player.GetComponent<BoxCollider2D>().enabled = false;
        GameObject panel = GameObject.FindGameObjectWithTag("ConvoPanel");
        panel.GetComponent<Animator>().Play("crashingText");
        player.transform.Find("Up").GetComponent<Animator>().Play("crashingUp");
        player.transform.Find("Left").GetComponent<Animator>().Play("crashingLeft");
        player.transform.Find("Right").GetComponent<Animator>().Play("crashingRight");
        player.transform.Find("Down").GetComponent<Animator>().Play("crashingDown");

        SoundMixer mixer = Camera.main.GetComponent<SoundMixer>(); //start scrambling the sound as well

        if (mixer != null)
        {
            mixer.changeChance = 1;
            mixer.ActivateScramble();
        }


        yield return new WaitForSeconds(7.0f); //wait for the animations to play out

        //Set up the rest of the vars so that the game is now Scrambled
        GlobalVars.isTutorial = false;
        GlobalVars.isCrashing = false;
        GoalTimes.ChangeScene();
        SceneManager.LoadScene(0);
    }
}
