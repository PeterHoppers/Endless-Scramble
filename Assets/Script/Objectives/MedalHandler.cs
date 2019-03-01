using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MedalHandler {

    static int bronzeNum;
    static int silverNum;
    static int goldNum;

    static bool[] goldArray; 
    static bool[] silverArray;
    static bool[] bronzeArray;

    //how much medals you need to unlock the next world
    static int[] worldUnlocks = { 5, 9, 14, 19, 23 };

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
        }
    }

    public static void CalcSilver()
    {
        if (!silverArray[GoalTimes.nextLevel]) //if silver has not been achieved
        {
            silverNum++;
            silverArray[GoalTimes.nextLevel] = true;
        }
    }

    public static void CalcBronze()
    {
        if (!bronzeArray[GoalTimes.nextLevel]) //if bronze has not been achieved
        {
            bronzeNum++;
            bronzeArray[GoalTimes.nextLevel] = true;
        }
    }

    public static void InitArrays()
    {
        goldArray = new bool[SceneManager.sceneCountInBuildSettings];
        silverArray = new bool[SceneManager.sceneCountInBuildSettings];
        bronzeArray = new bool[SceneManager.sceneCountInBuildSettings];
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

    public static int GetTotalMedals()
    {
        int totalMedals = GetGoldMedals() + GetSilverMedals() + GetBronzeMedals();
        return totalMedals;
    }

    public static int GetWorldUnlockNum(int worldNum)
    {
        worldNum--;
        return worldUnlocks[worldNum];
    }

    public static int GetMedalType(int levelNum)
    {
        if (goldArray[levelNum]) return 3;
        else if (silverArray[levelNum]) return 2;
        else if (bronzeArray[levelNum]) return 1;
        else return 0;
    }
}
