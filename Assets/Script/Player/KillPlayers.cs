using UnityEngine;
using System.Collections;

public class KillPlayers : MonoBehaviour {

    Transform respawnPoint;

    GameObject start;

    public void Death(GameObject deadPlayer)
    {
        if (deadPlayer.name.Equals("Player (1)"))
        {
            start = GameObject.Find("Start 1");
        }
        else if (deadPlayer.name.Equals("Player (2)"))
        {
            start = GameObject.Find("Start 2");
        }
        else if (deadPlayer.name.Equals("Player (3)"))
        {
            start = GameObject.Find("Start 3");
        }
        else if (deadPlayer.name.Equals("Player (4)"))
        {
            start = GameObject.Find("Start 4");
        }

        respawnPoint = start.transform;
        deadPlayer.GetComponent<PlayerStats>().lives--;
        deadPlayer.transform.position = new Vector3(respawnPoint.position.x, respawnPoint.position.y, deadPlayer.transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (this.gameObject.GetComponent<PlayerStats>().isChasing && collision.gameObject.GetComponent<PlayerStats>().isFleeing)
            {
                collision.gameObject.GetComponent<KillPlayers>().Death(collision.gameObject);
                this.gameObject.GetComponent<PlayerStats>().score++;
                //this.gameObject.GetComponent<PlayerStats>().isChasing = false;
                //collision.gameObject.GetComponent<PlayerStats>().isFleeing = false;
                //gameObject.GetComponent<MeshRenderer>().material.color = Camera.main.GetComponent<MultiplayerActivtion>().normalColor;
                //collision.gameObject.GetComponent<MeshRenderer>().material.color = Camera.main.GetComponent<MultiplayerActivtion>().normalColor;
            }
        }
    }
}
