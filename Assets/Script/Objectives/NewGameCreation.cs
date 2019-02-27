using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class NewGameCreation : MonoBehaviour {

    public static string introText = "NEW GAME";

    void Start()
    {
        GetComponentInChildren<Text>().text = introText;
    }

    public void CreateMedalBoolArrays()
    {
        GlobalVars.isMultiplayer = false;

        if (GlobalVars.numOfLoads == 0)
            GlobalVars.isTutorial = true;

        if (GlobalVars.isTutorial || GlobalVars.numOfLoads <= 1)
        {
            SceneManager.LoadScene("Tutorial");
            MedalHandler.InitArrays();
            GlobalVars.numOfLoads++;

            if (GlobalVars.numOfLoads > 1)
            {
                introText = "CNOTUINE";
                GetComponentInChildren<Text>().text = introText;
            }
        }
        else
        {
            SceneManager.LoadScene("mainMenu");
        }
        //SceneManager.LoadScene("tutorial");
    }
}
