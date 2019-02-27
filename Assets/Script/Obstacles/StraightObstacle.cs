using UnityEngine;
using System.Collections;

//Needs to go on a seperate prefab that you are pooling
public class StraightObstacle : MonoBehaviour {

    public GridObjectSpawner spawner;

    RectTransform endPosition;
    RectTransform trans;

	float speed;

    public bool isMovingX;
    public bool isMovingY;

    void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<GridObjectSpawner>();
        endPosition = spawner.endPosition;
		speed = spawner.speed;
        trans = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (isMovingX)
        {
            trans.position += Vector3.right * speed * Time.deltaTime;

            if (speed > 0) //detects if it is moving left or right
            {
                if (trans.position.x > endPosition.position.x)
                {
                    DestroyObject(gameObject);
                }
            }
            else
            {
                if (trans.position.x < endPosition.position.x)
                {
                    DestroyObject(gameObject);
                }
            }
        }
        else if (isMovingY)
        {
            trans.position += Vector3.up * speed * Time.deltaTime;

            if (speed > 0)
            {
                if (trans.position.y > endPosition.position.y)
                {
                    DestroyObject(gameObject);
                }
            }
            else
            {
                if (trans.position.y < endPosition.position.y)
                {
                    DestroyObject(gameObject);
                }
            }
        }
    }
}
