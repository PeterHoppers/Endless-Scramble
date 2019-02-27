using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultiplayerOptionUI : MonoBehaviour {

    public Text livesText;
    public Text pointsToWinText;
	
	// Update is called once per frame
	void Update ()
    {
        livesText.text = Camera.main.GetComponent<MultiplayerWinConditions>().players[0].GetComponent<PlayerStats>().lives.ToString();
        pointsToWinText.text = Camera.main.GetComponent<MultiplayerWinConditions>().scoreToWin.ToString();
    }
}
