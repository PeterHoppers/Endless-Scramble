using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//unlocks the next worlds if you have enough medals
public class UnlockNewLevels : MonoBehaviour {

    public int goldMetalsToUnlock;
    public GameObject closedPrompt;

	// Use this for initialization
	void Start () 
    {
        bool requiedMetals = (MedalHandler.GetGoldMedals() >= goldMetalsToUnlock);

        this.GetComponent<Button>().interactable = requiedMetals; //if you have enough medals, be able to click, if not, no click for you
        closedPrompt.SetActive(!requiedMetals); //prompt apprears if you do not have enough     
	}
}
