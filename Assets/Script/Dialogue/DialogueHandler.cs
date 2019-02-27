using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//mutliple of this, one of convoScript
public class DialogueHandler : MonoBehaviour 
{      //to use, create an empty gameobject, and place this script, and fill in the dialogue          *MAKE SURE TO ADD GAMEOBJECT TO THE CORRECT TARGET ARRAY*
    ConversationScript convoScriptText;
    [TextArea (2, 5)]
    public string[] dialogue;
    public Sprite[] faceArray;
    public bool advanceDialogue = false;

    // Use this for initialization
    void Start () 
	{
        convoScriptText = GameObject.FindGameObjectWithTag("ConvoUIText").GetComponent<ConversationScript>();

        if (convoScriptText == null)
            Debug.LogError("The text for the Conversation has been tagged incorrectly.");

        if (!GlobalVars.isTutorial) //don't scamble it if we're in the tutorial
        {
            string[] scrambledDialogue = new string[dialogue.Length];

            for (int index = 0; index < dialogue.Length; index++) //filter each piece of dialogue into the scamblers
            {
                scrambledDialogue[index] = ScrambleText.ScramblingText(dialogue[index]);
            }

            dialogue = scrambledDialogue; //set dialogue equal to the scrambled text
        }

        //set the components in convoScript to the ones in this dialogue handler
        convoScriptText.conversation = dialogue;
        convoScriptText.faceArray = faceArray;
	}
}
