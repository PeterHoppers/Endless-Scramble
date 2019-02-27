using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveGoalObstacle : MonoBehaviour
{
    Sprite blueImage;
    Sprite redImage;
    Sprite disabledImage;

    int currentPosition = 0;
    int lengthOfPositions;

    MovingGoalManager manager;

    public bool moveBack;
    public bool happenOnce;
    bool isDisabled;

    // Use this for initialization
    void Start()
    {
        manager = Camera.main.gameObject.GetComponent<MovingGoalManager>();

        if (manager == null)
            Debug.LogError("Camera doesn't have \"MovingGoalManager\" on it.");

        blueImage = manager.blueImage;
        redImage = manager.redImage;
        disabledImage = manager.disabledImage;
        lengthOfPositions = manager.possibleSpots.Length;

        SetImage();

        DeathManager.PlayerKilled += ResetMoveObstacle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (!isDisabled)
            {
                currentPosition = manager.currentPosition;

                if (moveBack)
                    currentPosition--;
                else
                    currentPosition++;

                if (currentPosition < 0)
                    currentPosition = (lengthOfPositions - 1);

                if (currentPosition >= lengthOfPositions)
                    currentPosition = 0;

                manager.currentPosition = currentPosition;

                manager.MovePositions();

                if (happenOnce)
                {
                    MoveGoalObstacle[] movers = transform.parent.GetComponentsInChildren<MoveGoalObstacle>();

                    foreach (MoveGoalObstacle move in movers)
                    {
                        move.isDisabled = true;
                        move.GetComponent<Image>().sprite = disabledImage;
                    }
                }
            }
        }
    }

    public void ResetMoveObstacle()
    {
        this.isDisabled = false;
        this.GetComponent<Image>().sprite = disabledImage;
        SetImage();
    }

    void SetImage()
    {
        if (moveBack)
            this.GetComponent<Image>().sprite = redImage;
        else
            this.GetComponent<Image>().sprite = blueImage;
    }

    private void OnDestroy()
    {
        DeathManager.PlayerKilled -= ResetMoveObstacle;
    }
}
