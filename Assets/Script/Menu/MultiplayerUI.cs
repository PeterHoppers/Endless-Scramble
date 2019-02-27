using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MultiplayerUI : MonoBehaviour {

    public GameObject player;
    public Text lives;
    public Text points;

	// Update is called once per frame
	void Update ()
    {
        lives.text = player.GetComponent<PlayerStats>().lives.ToString();
        points.text = player.GetComponent<PlayerStats>().score.ToString();

        if (player.GetComponent<PlayerStats>().lives <= 0)
        {
            this.transform.parent.GetComponent<Animator>().Play("fadePanel");
        }
    }
}
