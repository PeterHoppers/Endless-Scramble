using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Dedicated to Millar for his playtesting work
public class EscapeButton : MonoBehaviour
{
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!SceneManager.GetActiveScene().name.Equals("mainMenu"))
                SceneManager.LoadScene("mainMenu");
        }
            
	}
}
