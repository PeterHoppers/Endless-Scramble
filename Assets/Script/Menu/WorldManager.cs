using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

    public GameObject[] worlds;

	// Use this for initialization
	void Start ()
    { 
        for (int index = 0; index < worlds.Length; index++)
        {
            worlds[index].SetActive(false);
        }

        worlds[GlobalVars.currentWorld].SetActive(true);
	}
}
