using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GlobalVars : MonoBehaviour {

    public static bool isTutorial = true;
    public static bool isCrashing = false;
    public static bool isMultiplayer = false;

    public static int numOfLoads = 0;
    public static int currentWorld = 0;

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "titleScreen" && SceneManager.GetActiveScene().name != "Tutorial")
        {
            if (isTutorial)
            {
                isTutorial = false;
            }
            else
            {
                isTutorial = false;
            }
        }
       
    }
}
