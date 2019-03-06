using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridTransformer : MonoBehaviour
{
    public Node firstNode;
    public Node secondNode;

    public GridObject imageHor;
    public GridObject imageVer;

    Node[][] nodes;
    // Use this for initialization
    void Start()
    {
        nodes = GetComponent<GridCreation>().nodes;
    }
    // Update is called once per frame
    void Update() {

    }

    public void TransformLine()
    {
        if (firstNode == null || secondNode == null)
        {
            Debug.LogError("There is not two nodes to transform between.");
        }

        byte h1 = firstNode.hIndex;
        byte h2 = secondNode.hIndex;
        byte w1 = firstNode.wIndex;
        byte w2 = secondNode.wIndex;

        if (h1 == h2)
        {
            if (w1 > w2)
                TransformRow(h1, w2, w1);
            else
                TransformRow(h1, w1, w2);
            return;
        }

        if (w1 == w2)
        {
            if (h1 > h2)
                TransformColumn(w1, h2, h1);
            else
                TransformColumn(w1, h1, h2);

            return;
        }

        Debug.LogError("Neither the row or column matches.");
    }

    void TransformRow(byte row, byte index1, byte index2)
    {
        CheckNodesExistence();

        Node[] nodeRow = nodes[row];

        for (int i = 0; i < nodeRow.Length; i++)
        {
            if (i >= index1 && i <= index2)
            {
                Node usedNode = nodeRow[i];

                if (usedNode != null)
                    TransformObject(usedNode, imageHor);      
            }
        }
    }

    void TransformColumn(byte column, byte index1, byte index2)
    {
        CheckNodesExistence();

        for (int i = 0; i < nodes.Length; i++)
        {
            if (i >= index1 && i <= index2)
            {
                Node usedNode = nodes[i][column];
                TransformObject(usedNode, imageVer);
            }
                
        }
    }

    void TransformObject(Node node, GridObject go)
    {
        GridObject gridObject = Instantiate(go, node.gameObject.transform.parent);
        gridObject.hIndex = node.hIndex;
        gridObject.wIndex = node.wIndex;

        RectTransform rectTransform = node.GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            gridObject.ChangeTransform(rectTransform.sizeDelta.y, rectTransform.sizeDelta.x);
        }
    }

    void CheckNodesExistence()
    {
        if (nodes == null)
            nodes = GetComponent<GridCreation>().nodes;

        if (nodes == null)
        {
            GetComponent<GridCreation>().FillGrid();
            nodes = GetComponent<GridCreation>().nodes;
        }
    }
}

