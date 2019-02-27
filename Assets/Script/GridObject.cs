using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    //hIndex and wIndex correspond to nodes in the grids
    public byte hIndex;
    public byte wIndex;
    [HideInInspector]
    public GridCreation grid;
    RectTransform rectTrans;

    //Grab the grid to make sure it does grab a connection with an object that doesn't exist in scene
    public void SetGrid(GridCreation grid)
    {
        this.grid = grid;
        GridCreation.GridAdjusted += ChangeTransform;
    }

    //Grabs the height and width provided from the GridCreation script and sets its
    //own size to match that. Adjusts its collider
    public virtual void ChangeTransform(float height, float width)
    {
        if (grid == null)
            grid = GameObject.FindGameObjectWithTag("Grid").GetComponent<GridCreation>();

        //adjust position to match up location based up the indexs it has
        rectTrans = GetComponent<RectTransform>();

        if (rectTrans != null)
        {
            rectTrans.sizeDelta = new Vector2(width, height);
            Vector2 pos = grid.ReturnPositionFromIndex(hIndex, wIndex);
            rectTrans.localPosition = pos;
        }

        //adjust its collider to match the new size of the object
        //the reason that the minus 2s are there are to provide a bit
        //of leeway on collisions. If the colliders feel a bit too big
        //players get frustrated: "I totally dodged that!"
        //if they are on the smaller size, you get the opposite effect
        //players get excited: "I can't believe I dodged that!"
        BoxCollider2D box = GetComponent<BoxCollider2D>();

        if (box != null)
            box.size = new Vector2(width - 2f, height - 2f);
    }

    //Simple method implemented to cut down on the number of lines
    public Vector2 GetPosition()
    {
        rectTrans = GetComponent<RectTransform>();

        if (rectTrans == null)
            return new Vector2(0, 0);
        else
            return rectTrans.position;
    }

    //Unsubscribe to the event if the object is destoryed as always
    private void OnDestroy()
    {
        GridCreation.GridAdjusted -= ChangeTransform;
    }
}
