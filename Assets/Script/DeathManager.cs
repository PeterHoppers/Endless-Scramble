using UnityEngine;

//Asssigned to the Main Camera
public class DeathManager : MonoBehaviour
{
    public delegate void OnPlayerKilled();
    public static event OnPlayerKilled PlayerKilled;
    
    //Calls out the event to any scripts that are listening
    public void PlayerDied()
    {
        if (PlayerKilled != null)
            PlayerKilled();
    }
}
