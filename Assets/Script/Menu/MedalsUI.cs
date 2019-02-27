using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MedalsUI : MonoBehaviour {

    public Text goldText;
    public Text silverText;
    public Text bronzeText;
   
    void Start ()
    {
        goldText.text = MedalHandler.GetGoldMedals().ToString();
        silverText.text = MedalHandler.GetSilverMedals().ToString();
        bronzeText.text = MedalHandler.GetBronzeMedals().ToString();
    }
	
	
}
