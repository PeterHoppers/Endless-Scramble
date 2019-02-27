using UnityEngine;

public class GridCreation : MonoBehaviour
{
    //Input how many nodes you want high and how many wide
    public byte numPerHeight;
    public byte numPerWidth;

    //the image you want to node to look like
    public Node gridImage;

    //a place to store all the nodes after they've been created
    public Node[][] nodes;

    //determines if the list should be repopulated if connection is lost
    //or if the series of nodes need to be simply created
    bool isNodesCreated;

    //A event call that is attached to all the nodes and GridObjects
    //in the game. This allows them to properly adjust their size
    //whenever need be
    public delegate void OnGridAdjusted(float width, float height);
    public static event OnGridAdjusted GridAdjusted;

    //When the scene loads, set the size of it to match the current screen
    private void Start()
    {
        BuildGrid(new Vector2(Screen.width, Screen.height));
    }

    //The method that actually builds the grid itself
    public void BuildGrid(Vector2 dimensions)
    {
        //if there is nothing there, create the grid
        if (transform.childCount == 0)
        {
            CreateGrid();
        }
        else
        {
            isNodesCreated = true;
            CreateGrid();
            FillGrid();
        }

        //calc the height and width based upon the dimesnsions given and the number of
        //nodes that it wants to create
        float height = (dimensions.y / numPerHeight);
        float width = (dimensions.x / numPerWidth);

        //0,0 is techically the middle point. In order to start at the bottom left at then fill
        //in the screen, we need to do some division in order to get there
        float hStartPoint = (-dimensions.y / 2);
        float wStartPoint = (-dimensions.x / 2);

        //the index we use for height positioning
        byte hIndex = 0;

        //going from the bottom left point, we add the height to the hSpawn, moving
        //us up the screen as we go. When we go too far off the screen, the loop breaks
        for (float hSpawn = hStartPoint; hSpawn < (dimensions.y / 2) + height; hSpawn += height)
        {
            if (hIndex > numPerHeight) break; //a failsafe just in case our rounding is off

            //the index we use for wisth positioning
            byte wIndex = 0;
            //going from the bottom left point, we add the width to the wSpawn, moving
            //us across the screen as we go. When we go too far off the screen, the loop breaks
            for (float wSpawn = wStartPoint; wSpawn < (dimensions.x / 2) + width; wSpawn += width)
            {
                Node newImage;

                if (wIndex > numPerWidth) break; //a failsafe just in case our rounding is off

                //create them and assign them the indexs
                if (!isNodesCreated)
                {
                    newImage = Instantiate(gridImage, transform);
                    newImage.hIndex = hIndex;
                    newImage.wIndex = wIndex;
                    nodes[hIndex][wIndex] = newImage;
                }
                else //populate if already created
                {
                    newImage = nodes[hIndex][wIndex];
                }

                //if we somehow have a null here (most likely because of the else)
                //continue to the next spot. Most likely an issue with being too close
                //to the edge
                if (newImage == null) continue;

                //set the position, size, and name for this new node
                newImage.GetComponent<RectTransform>().localPosition = new Vector3(wSpawn, hSpawn, 0);
                RectTransform rect = newImage.GetComponent<RectTransform>();

                rect.sizeDelta = new Vector2(width, height);

                newImage.name = "Image: " + hIndex + " " + wIndex;

                wIndex++;
            }

            hIndex++;
        }

        //loop through each of the children and set its connection to this grid
        foreach (Transform child in transform)
        {
            GridObject gridObj = child.GetComponent<GridObject>();
            if (gridObj == null)
                continue;

            gridObj.SetGrid(this);
        }

        //call the event that alerts the gridObjects that this has changed size
        if (GridAdjusted != null)
            GridAdjusted(height, width);
    }

    //Instaiate the jagged array if it doesn't exist
    void CreateGrid()
    {
        nodes = new Node[numPerHeight + 1][];
        for (int i = 0; i < numPerHeight + 1; i++) nodes[i] = new Node[numPerWidth + 1];
    }

    //If the nodes have already been made and the connection has been lost
    //this method refills the node jagged array back up
    public void FillGrid()
    {
        foreach (Transform child in transform)
        {
            Node node = child.GetComponent<Node>();
            if (node == null)
                continue;

            if (nodes == null)
                CreateGrid();

            nodes[node.hIndex][node.wIndex] = node;
        }
    }

    //This destorys the grid that has been made, resetting variables that have been made in the process
    public void DeleteGrid()
    {
        //loop through all the children of this gameObject and delete them
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }

        nodes = null;
        isNodesCreated = false;
    }

    //Convert a set of indexs into a Vector2 based upon the position of that node
    public Vector2 ReturnPositionFromIndex(byte hIndex, byte wIndex)
    {
        //quick check to make sure the place is full
        FillGrid();

        //grab the needed node and then take its RectTransform that holds information about its position
        RectTransform rectTrans = nodes[hIndex][wIndex].GetComponent<RectTransform>();

        if (rectTrans == null)
            return Vector2.zero;
        else
            return rectTrans.localPosition;
    }
}
