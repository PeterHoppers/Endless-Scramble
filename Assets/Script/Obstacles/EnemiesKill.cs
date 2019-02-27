using UnityEngine;

public class EnemiesKill : MonoBehaviour
{
    DeathManager manager; //reference to the lone death mamanger

    void Start()
    {
        manager = Camera.main.GetComponent<DeathManager>(); //always attached to the camera

        //double check that the collider attached to it is trigger. If it is not, the following method
        //can not run, thus never killing/resetting the player
        if (GetComponent<BoxCollider2D>())
            GetComponent<BoxCollider2D>().isTrigger = true; 
        else
            Debug.LogError("An ememy object does not have a collider on it");
    }

    //Activates when something enter the trigger attached the the enemy gameObject
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if what hit this trigger was indeed the player
        if (collision.gameObject.tag.Equals("Player"))
        {
            manager.PlayerDied();
        }
    }
}
