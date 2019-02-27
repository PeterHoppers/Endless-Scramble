using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoalTimes : MonoBehaviour {

	public static List<float> timesGoal = new List<float> ();
	public static int nextLevel;
	static float totalTime;

    public delegate void OnLevelChanged();
    public static event OnLevelChanged LevelChanged;

    public static void AddTime(float time)
	{
		timesGoal.Add (time);
	}

	public static void PrintTimes()
	{
		for (int cnt = 0; cnt < timesGoal.Count; cnt ++)
		{
			print (timesGoal[cnt]);
		}
	}

	public static float SumTimes()
	{
		for (int cnt = 0; cnt < timesGoal.Count; cnt ++)
		{
			totalTime += (timesGoal[cnt]);
		}

		return totalTime;
	}

    public static void ChangeScene()
    {
        if (LevelChanged != null)
            LevelChanged();
    }
}
