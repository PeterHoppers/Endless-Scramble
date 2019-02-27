using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConversationScript : MonoBehaviour {

    public string[] conversation;    
    Text textBox;
    public Image face;
    public float writeSpeed = 0.01f;

    public AudioSource sound;

    public Sprite[] faceArray;

    [HideInInspector]
    public int convoIndex = 0;
    int maxconvoIndex;
    [HideInInspector]
    public bool convoDone = false;
    string text;

    Text advanceText;
    Animator anim;

    public bool textDone = false;

    // Use this for initialization
    void Start () 
	{
        anim = transform.parent.GetComponent<Animator>(); //attempt to grab this animator

        if (anim == null)
            Debug.LogError("There's no animator attached to the dialgoue box.");

        advanceText = transform.parent.Find("AdvanceText").GetComponent<Text>(); //find the advance text prompt from this gameobject
        SetTextDoneState(convoDone);

        textBox = this.GetComponent<Text>(); //grab the text component
        //set the image and text to the first image and words respectively
        text = conversation[0];
		face.sprite = faceArray[0];

        StartCoroutine(TypeWriter());       //start typing out the words 
    }
	
	// Update is called once per frame
	void Update () 
	{
        //--------------Figuring Out How To Move the Text Along (Whether Buttons are Active Or Not-------------
        if (textDone && !convoDone && ((Input.GetKeyDown("e") || convoIndex == 0)))
		{
			ConvoHandler();                
		}

        if (GlobalVars.isCrashing)
        {
            textDone = false;
            this.enabled = false;
        }
    }

    public bool ConvoMatchesIndex()
    {
        return (convoIndex == 1);
    }

    IEnumerator TypeWriter() //prints out the letters one at a time like a typewriter
    {
        maxconvoIndex = conversation.Length;
        for (int i = 0; i <= text.Length; i++ )
        {
            textBox.text = text.Substring(0, i);

            if (sound != null) //if there is sound attach, play it
                sound.Play();

            if (GlobalVars.isTutorial) //play at a regular interval
                yield return new WaitForSeconds(.01f);
            else //play at random intervals
            {
                float ranTime = Random.Range(.005f, .02f);
                yield return new WaitForSeconds(ranTime);
            }
        }
       	convoIndex++;

        //------Reset The ConvoIndex When Max Is Hit----------
        if (convoIndex == maxconvoIndex)
        {
            convoDone = true; //ends the conversation
            convoIndex = 0;
        }

        SetTextDoneState(true);
    }

    //Bring in the next line of dialogue and face image
    void ConvoHandler()
    {
        text = conversation[convoIndex];
        face.sprite = faceArray[convoIndex];
        
		if (faceArray[convoIndex] == null)
        {
            face.enabled = false;
        }
        else
        {
            face.enabled = true;
        }

        SetTextDoneState(false);
        StartCoroutine(TypeWriter());
    }

    //Updates all the elements that correspond with the printing of the text being either done or not
    void SetTextDoneState(bool isDone)
    {
        textDone = isDone;

        anim.SetBool("textDone", textDone);

        advanceText.enabled = isDone;
    }
}