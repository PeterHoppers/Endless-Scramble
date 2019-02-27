using UnityEngine;
using System.Collections;

//Needs to go on a GameObject called "EnemySpawner" in the Enemies tap.
public class StraightObstacleSpawn : MonoBehaviour {

    public RectTransform startPosition;
    public RectTransform endPosition;
    public float spawnFreq;
	public float speed;
    float lastShot;

    void Update()
    {
        if ((Time.time > lastShot + spawnFreq) || Time.time == 0)
        {
            SpawnPhase();
        }
    }

    void SpawnPhase()
    {
        lastShot = Time.time;

        GameObject obj = PoolingObstacle.current.GetPooledObject();

        if (obj == null)
        {
            return;
        }

        obj.transform.SetParent(this.transform);
        obj.GetComponent<GridObject>().ChangeTransform(GameObject.FindGameObjectWithTag("Player").GetComponent<RectTransform>().sizeDelta.x, GameObject.FindGameObjectWithTag("Player").GetComponent<RectTransform>().sizeDelta.y);
        obj.GetComponent<RectTransform>().position = startPosition.position;
        //obs.spawner = this;
        obj.SetActive(true);
    }

}
