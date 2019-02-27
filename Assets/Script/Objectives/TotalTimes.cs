using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TotalTimes : MonoBehaviour {

	public Text[] textArray;

	public Text textTotal;
	// Use this for initialization
	void Start () 
	{
		for (int cnt = 0; cnt < textArray.Length; cnt ++)
		{
			textArray[cnt].text = "Level " + (cnt + 1) + " : " + GoalTimes.timesGoal[cnt].ToString();
		}
		
		textTotal.text = "Total Time: " + GoalTimes.SumTimes().ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
