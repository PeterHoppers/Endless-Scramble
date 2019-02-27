using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMixer : MonoBehaviour
{
    AudioSource audioSource;
    AudioClip clip;

    public int changeChance;

    float length;
    static int currentPosition;

    int randomNum;

	// Use this for initialization
	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;

        clip = audioSource.clip;

        if (clip == null)
            return;

        length = clip.length;

        if (currentPosition > length)
            currentPosition = 0;

        if (!GlobalVars.isTutorial)
            ActivateScramble();
	}

    public void ActivateScramble()
    {
        InvokeRepeating("ScrambleSong", 0f, 1f);
    }

    void ScrambleSong()
    {
        randomNum = Random.Range(0, changeChance);

        if (randomNum != 0)
            return;

        randomNum = (int) Random.Range(0, length);

        audioSource.time = randomNum;
    }
}
