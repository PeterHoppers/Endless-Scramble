using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultiplayerWinConditions : MonoBehaviour {

    public int startingLives = 3;
    public int scoreToWin = 5;
    public Text winningText;
    public GameObject[] players;
    public GameObject playerCanvas;

    public int playersAlive;

    void Start()
    {
        winningText.transform.parent.gameObject.SetActive(false);
    }

    public void WinningCelebration(GameObject winner)
    {
        for (int index = 0; index < players.Length; index++)
            players[index].GetComponentInChildren<Movement>().enabled = false;

        winningText.transform.parent.gameObject.SetActive(true);
        winningText.text = " " + winner.name + " is the winner!";
    }

    void Update()
    {
        //=================Checks to See Who is the Last One============
        if (playersAlive <= 1 && playerCanvas.activeSelf)
        {
            if (players[0].activeSelf)
            {
                GameObject winningPlayer = players[0];
                WinningCelebration(winningPlayer);
            }
            else if (players[1].activeSelf)
            {
                GameObject winningPlayer = players[1];
                WinningCelebration(winningPlayer);
            }
            else if (players[2].activeSelf)
            {
                GameObject winningPlayer = players[2];
                WinningCelebration(winningPlayer);
            }
            else if (players[3].activeSelf)
            {
                GameObject winningPlayer = players[3];
                WinningCelebration(winningPlayer);
            }
            else
            {
                WinningCelebration(null);
            }
        }
    }
}
