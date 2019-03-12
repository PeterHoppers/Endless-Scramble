using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//Place this script on the camera//
public class MovingGoalManager : MonoBehaviour {

    public Vector2[] possibleSpots;

    public bool bringInCurrentPositions;

    public Sprite blueImage;
    public Sprite redImage;
    public Sprite disabledImage;

    public GridObject blueGoal;
    public GridObject redGoal;

    GridObject goal;

    public int currentPosition = 0;

    // Use this for initialization
    void Start ()
    {
        goal = GameObject.FindGameObjectWithTag("Goal").GetComponent<GridObject>();

        if (goal == null)
            Debug.LogError("Goal is either missing or not properly tagged.");

        DeathManager.PlayerKilled += ResetPositions;

        if (bringInCurrentPositions)
        {
            possibleSpots[0] = (Vector2)goal.GetPosition();
            possibleSpots[1] = (Vector2)blueGoal.GetPosition();

            if (possibleSpots.Length > 2)
                possibleSpots[2] = (Vector2)redGoal.GetPosition();

        }

        if (possibleSpots.Length >= 1)
            MovePositions();
    }

    public void MovePositions()
    {
        goal.transform.position = possibleSpots[currentPosition];

        if (currentPosition + 1 < possibleSpots.Length)
            blueGoal.transform.position = possibleSpots[(currentPosition + 1)];
        else
            blueGoal.transform.position = possibleSpots[0];

        if (possibleSpots.Length > 2)
        {
            if ((currentPosition - 1) >= 0)
                redGoal.transform.position = possibleSpots[(currentPosition - 1)];
            else
                redGoal.transform.position = possibleSpots[(possibleSpots.Length - 1)];
        }
    }

    public void ResetPositions()
    {
        if (possibleSpots.Length <= 0)
            return;

        currentPosition = 0;

        goal.transform.position = possibleSpots[0];
        blueGoal.transform.position = possibleSpots[1];

        if (possibleSpots.Length > 2)
            redGoal.transform.position = possibleSpots[2];
    }

    private void OnDestroy()
    {
        DeathManager.PlayerKilled -= ResetPositions;
    }
}
