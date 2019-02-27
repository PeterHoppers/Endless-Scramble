using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TriggerText : MonoBehaviour {

	[HideInInspector]
    Sprite[] faceArray;

    string[] temp;
    Sprite[] faceTemp;

    public DialogueHandler[] targetArray;
	//[HideInInspector]
    public int index = 0;

    //Dialogue Handler variables
    [HideInInspector]
    public string[] dialogue;
    [HideInInspector]
    bool advanceDialogue = true;

    GameObject panel;
    DialogueHandler target;
    ConversationScript convoScript;
    Text text;
    DialogueButton diaButton;

    void Start()
    {
        panel = GameObject.FindGameObjectWithTag("ConvoPanel");

        if (panel == null)
            Debug.LogError("Convoseration Panel isn't tagged correctly");

        text = panel.transform.Find("ConvoText").GetComponent<Text>();

        if (text != null)
            convoScript = text.GetComponent<ConversationScript>();

        diaButton = panel.transform.Find("Button").GetComponent<DialogueButton>();

        if (GlobalVars.isTutorial)
            index = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (index >= targetArray.Length)
        {
            index = 0;
        }
        else if (index < targetArray.Length)
        {
            GetDialogue();
            PassDialogue();
        }

        if ((convoScript.convoDone) && (Input.GetKeyDown("e")))              //this runs when the dialogue is done
        {
            EndConvo();
        }

    }

    void GetDialogue()
    {        
		faceArray = new Sprite[dialogue.Length];
        target = targetArray[index];
        dialogue = target.dialogue;
        faceArray = target.faceArray;
        temp = new string[dialogue.Length];
        faceTemp = new Sprite[faceArray.Length];      
    }

    //Pass the dialogue and faceArray into convoScript to be displayed
    void PassDialogue()
    {
        convoScript.conversation = dialogue;
        convoScript.faceArray = faceArray;
    }

    //Turns off text box, enables player movement, resets conversationscript index
    void EndConvo()
	{
        diaButton.SwitchingState();
		convoScript.convoIndex = 0;
        convoScript.convoDone = false;
        dialogue = temp;
		faceArray = faceTemp;
		AdvanceDialogue ();
    }

    //Turns on text box and disables player movement
    void AdvanceDialogue()
    {
        if (advanceDialogue)
        {
            index++;
            StartCoroutine(ClearText());
        }
        else
            StartCoroutine(ClearText());
    }

    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(.5f);
        text.text = "_";
    }
}