using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//unlocks the next worlds if you have enough medals
public class UnlockNewLevels : MonoBehaviour {

    [Tooltip("The number of medals needed to unlock the next world")]
    public int worldUnlock;
    public GameObject closedPrompt;

	// Use this for initialization
	void Start () 
    {
        int totMetalsToUnlock = MedalHandler.GetWorldUnlockNum(worldUnlock);
        bool doesPlayerHaveMedals = (MedalHandler.GetTotalMedals() >= totMetalsToUnlock);

        if (!doesPlayerHaveMedals)
        {
            int diff = totMetalsToUnlock - MedalHandler.GetTotalMedals();
            Text text = closedPrompt.GetComponentInChildren<Text>();

            if (text == null)
                return;

            string msg = "Need " + diff + " More Medals To Unlock the Next World";

            text.text = msg;
        }

        this.GetComponent<Button>().interactable = doesPlayerHaveMedals; //if you have enough medals, be able to click, if not, no click for you
        closedPrompt.SetActive(!doesPlayerHaveMedals); //prompt apprears if you do not have enough     
	}
}
