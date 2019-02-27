using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

    [HideInInspector]
    public int lives;
    public int score;
    public bool isFleeing;
    public bool isChasing;

    public Material fleeingMaterial;
    public Material chasingMaterial;
    public Material bothMaterial;
    public Material normalMaterial;



    [HideInInspector]
    public int amtFleeing;
    [HideInInspector]
    public int amtChasing;

    int pointsToWin;

    void Start()
    {
        pointsToWin = Camera.main.GetComponent<MultiplayerWinConditions>().scoreToWin;
        lives = Camera.main.GetComponent<MultiplayerWinConditions>().startingLives;
        gameObject.GetComponent<MeshRenderer>().material = normalMaterial;
    }
    void Update()
    {
        if (lives <= 0)
        {
            gameObject.SetActive(false);
            Camera.main.GetComponent<MultiplayerWinConditions>().playersAlive--;
        }

        if (score >= pointsToWin)
        {
            Camera.main.GetComponent<MultiplayerWinConditions>().WinningCelebration(gameObject);
        }
    }
}
