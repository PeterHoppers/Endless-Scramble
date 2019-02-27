using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MedalHandler : MonoBehaviour {

    static int bronzeNum;
    static int silverNum;
    static int goldNum;

    static bool[] goldArray; 
    static bool[] silverArray;
    static bool[] bronzeArray; 

    public static int CalcMedalType(float bronzeTime, float silverTime, float goldTime, float totalTime)
    { 
        int medalTypeNum = 0;

        if (totalTime <= bronzeTime) //wins bronze medal
        {
            medalTypeNum = 1;
            CalcBronze();
        }
        if (totalTime <= silverTime) //wins silver medal
        {
            medalTypeNum = 2;
            CalcSilver();
        }
        if (totalTime <= goldTime) //wins gold medal
        {
            medalTypeNum = 3;
            CalcGold();
        }

        return medalTypeNum;
            
    }

    public static void CalcGold()
    {
        if (!goldArray[GoalTimes.nextLevel]) //if gold has not been achieved
        {
            goldNum++;
            goldArray[GoalTimes.nextLevel] = true;
            print("get a gold");
        }
        else
            print("Gold Medal Already Aquired.");
    }

    public static void CalcSilver()
    {
        if (!silverArray[GoalTimes.nextLevel]) //if silver has not been achieved
        {
            silverNum++;
            silverArray[GoalTimes.nextLevel] = true;
            print("get a silver");
        }
        else
            print("Silver Medal Already Aquired.");
    }

    public static void CalcBronze()
    {
        if (!bronzeArray[GoalTimes.nextLevel]) //if bronze has not been achieved
        {
            bronzeNum++;
            bronzeArray[GoalTimes.nextLevel] = true;
            print("get a bronze");
        }
        else
            print("Bronze Medal Already Aquired");
    }

    public static void InitArrays()
    {
        goldArray = new bool[SceneManager.sceneCountInBuildSettings];
        silverArray = new bool[SceneManager.sceneCountInBuildSettings];
        bronzeArray = new bool[SceneManager.sceneCountInBuildSettings];
        print("CLICKED! NEW GAME ACTIVATED!");
    }

    public static int GetGoldMedals()
    {
        return goldNum;
    }

    public static int GetSilverMedals()
    {
        return silverNum;
    }

    public static int GetBronzeMedals()
    {
        return bronzeNum;
    }
}
