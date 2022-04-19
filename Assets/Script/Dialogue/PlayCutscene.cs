using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

public class PlayCutscene : MonoBehaviour {

    public VideoPlayer movie;

    bool isPlaying;
	// Use this for initialization
	void Start ()
    {
        //GetComponent<RawImage>().texture = movie as VideoPlayer;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isPlaying)
        {
            print("playing");
            isPlaying = true;
            movie.Play();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPlaying)
        {
            print("pausing");
            isPlaying = false;
            movie.Pause();
        }
	}
}
