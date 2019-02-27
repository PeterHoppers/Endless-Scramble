using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayCutscene : MonoBehaviour {

    public MovieTexture movie;

    bool isPlaying;
	// Use this for initialization
	void Start ()
    {
        GetComponent<RawImage>().texture = movie as MovieTexture;
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
