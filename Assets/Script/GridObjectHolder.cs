using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObjectHolder : GridObject
{
    public byte numOfObjects;
    public GridObject gridObject;
    public bool isHorizontal;

    public override void ChangeTransform(float height, float width)
    {
        if (grid == null)
            grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridCreation>();

        Vector2 pos = grid.ReturnPositionFromIndex(hIndex, wIndex);
        GetComponent<RectTransform>().localPosition = pos;

        for (int i = 0; i < numOfObjects; i++)
        {
            GridObject gridObjCreated = Instantiate(gridObject, transform);
            RectTransform rectTrans = gridObjCreated.GetComponent<RectTransform>();

            if (rectTrans != null)
            {
                rectTrans.sizeDelta = new Vector2(width, height);
                ///Find Needed Distance From Middle
                int middleNum = numOfObjects / 2;

                int offsetNum = i - middleNum;

                float posOffset;

                if (!isHorizontal)
                {
                    posOffset = height * offsetNum;
                    rectTrans.localPosition = new Vector2(0, posOffset);
                }                    
                else
                {
                    posOffset = width * offsetNum;
                    rectTrans.localPosition = new Vector2(posOffset, 0);
                }
                    
            }

            BoxCollider2D box = gridObjCreated.GetComponent<BoxCollider2D>();

            if (box != null)
            {
                if (isHorizontal)
                    box.size = new Vector2(width - 2f, height - 10f);
                else
                    box.size = new Vector2(width - 10f, height - 2f);
            }
                

        }
    }
}
