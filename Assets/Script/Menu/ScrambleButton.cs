using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrambleButton : MonoBehaviour {

    Text text;
	// Use this for initialization
	void Start ()
    {
        text = GetComponentInChildren<Text>();

        if (text == null)
            text = GetComponent<Text>();

        if (!GlobalVars.isTutorial)
            text.text = ScrambleText.ScramblingText(text.text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
