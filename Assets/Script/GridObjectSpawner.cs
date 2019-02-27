using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectSpawner : GridObject
{
    public GridObject gridObject;
    public RectTransform startPosition;
    public RectTransform endPosition;
    public float spawnFreq;
    public float speed;
    float lastShot;

    GridObject objCopy;
    float objHeight;
    float objWidth;

    public override void ChangeTransform(float height, float width)
    {
        if (grid == null)
            grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridCreation>();

        objWidth = width;
        objHeight = height;
    }

    private void Start()
    {
        objCopy = Instantiate(gridObject, transform);
        objCopy.gameObject.SetActive(false);
    }

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

        GridObject gridObjCreated = Instantiate(objCopy, transform);

        if (gridObjCreated == null)
        {
            return;
        }

        gridObjCreated.gameObject.SetActive(true);

        RectTransform rectTrans = gridObjCreated.GetComponent<RectTransform>();

        if (rectTrans != null)
        {
            rectTrans.sizeDelta = new Vector2(objWidth, objWidth);
            Node startNode = startPosition.GetComponent<Node>();
            Vector2 pos = grid.ReturnPositionFromIndex(startNode.hIndex, startNode.wIndex);
            rectTrans.localPosition = pos;
        }

        GridObjectHolder gridHolder = gridObjCreated.GetComponent<GridObjectHolder>();

        if (gridHolder != null)
        {
            Node startNode = startPosition.GetComponent<Node>();
            gridHolder.hIndex = startNode.hIndex;
            gridHolder.wIndex = startNode.wIndex;

            gridHolder.ChangeTransform(objHeight, objWidth);
        }


        //gridObjCreated.GetComponent<RectTransform>().position = startPosition.position;
        StraightObstacle obs = gridObjCreated.GetComponent<StraightObstacle>();
        //obs.endPosition = endPosition;

        BoxCollider2D box = gridObjCreated.GetComponent<BoxCollider2D>();

        if (box != null)
            box.size = new Vector2(objWidth - .5f, objHeight - .5f);

        obs.spawner = this;
    }
}
