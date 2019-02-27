using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridTransformer))]
public class GridTransformerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridTransformer gridTrans = (GridTransformer)target;

        if (GUILayout.Button("Transfrom Line"))
        {
            gridTrans.TransformLine();
        }
    }
}
