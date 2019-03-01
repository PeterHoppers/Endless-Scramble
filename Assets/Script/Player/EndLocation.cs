using UnityEngine;

public class EndLocation : MonoBehaviour
{
    TimeTracking timeTracking;

    private void Start()
    {
        timeTracking = Camera.main.GetComponent<TimeTracking>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            timeTracking.FinishedLevel();
        }
    }
}
