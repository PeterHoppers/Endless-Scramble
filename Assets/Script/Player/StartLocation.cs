using UnityEngine;
using System.Collections;

public class StartLocation : MonoBehaviour {

    public GameObject _player;
	// Use this for initialization
	
    void Start () 
    {
        if (!GlobalVars.isMultiplayer)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _player.GetComponent<Movement>().ResetPostion();
        }
        else
            _player.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
    }
}
