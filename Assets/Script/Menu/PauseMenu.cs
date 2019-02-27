using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenu;

    bool isPaused;

	// Use this for initialization
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseActivation();
	}

    public void PauseActivation()
    {
        if (!GlobalVars.isTutorial)
        {
            isPaused = pauseMenu.activeSelf;

            isPaused = !isPaused;

            if (isPaused)
            {
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
            }
        }
    }

}
