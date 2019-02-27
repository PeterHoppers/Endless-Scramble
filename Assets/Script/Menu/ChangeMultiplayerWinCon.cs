using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeMultiplayerWinCon : MonoBehaviour {


    public void AddPointsToWin()
    {
        Camera.main.GetComponent<MultiplayerWinConditions>().scoreToWin++;

        if (Camera.main.GetComponent<MultiplayerWinConditions>().scoreToWin >= 99)
            Camera.main.GetComponent<MultiplayerWinConditions>().scoreToWin = 99;
    }

    public void SubtractPointsToWin()
    {
        Camera.main.GetComponent<MultiplayerWinConditions>().scoreToWin--;

        if (Camera.main.GetComponent<MultiplayerWinConditions>().scoreToWin <= 1)
            Camera.main.GetComponent<MultiplayerWinConditions>().scoreToWin = 1;
        else
            this.GetComponent<Button>().interactable = true;
    }

    public void AddStartingLives()
    {
        for (int index = 0; index < 4; index++)
            Camera.main.GetComponent<MultiplayerWinConditions>().players[index].GetComponent<PlayerStats>().lives++;

        if (Camera.main.GetComponent<MultiplayerWinConditions>().players[0].GetComponent<PlayerStats>().lives >= 99)
            Camera.main.GetComponent<MultiplayerWinConditions>().players[0].GetComponent<PlayerStats>().lives = 99;
    }

    public void SubstractStartingLives()
    {
        for (int index = 0; index < 4; index++)
            Camera.main.GetComponent<MultiplayerWinConditions>().players[index].GetComponent<PlayerStats>().lives--;

        if (Camera.main.GetComponent<MultiplayerWinConditions>().players[0].GetComponent<PlayerStats>().lives <= 1)
            Camera.main.GetComponent<MultiplayerWinConditions>().players[0].GetComponent<PlayerStats>().lives = 1;
    }
}
