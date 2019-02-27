using UnityEngine;
using System.Collections;

public class WorldScroller : MonoBehaviour {

    WorldManager worldManager;

    public bool nextWorld;

    void Start()
    {
        worldManager = GameObject.FindGameObjectWithTag("WorldManager").GetComponent<WorldManager>();
        //CheckWorldNumber();
    }

    //void CheckWorldNumber()
    //{
    //    if (nextWorld)
    //    {

    //        if (GlobalVars.currentWorld >= (worldManager.worlds.Length - 1))
    //            this.gameObject.SetActive(false);
    //        else
    //            this.gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        if (GlobalVars.currentWorld <= 0)
    //            this.gameObject.SetActive(false);
    //        else
    //            this.gameObject.SetActive(true);
    //    }

    //}

    public void MoveWorld()
    {
        worldManager.worlds[GlobalVars.currentWorld].SetActive(false);

        if (nextWorld)
            GlobalVars.currentWorld++;
        else
            GlobalVars.currentWorld--;

        worldManager.worlds[GlobalVars.currentWorld].SetActive(true);

        //CheckWorldNumber();
    }
}
