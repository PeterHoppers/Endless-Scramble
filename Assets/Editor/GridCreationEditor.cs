using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridCreation))]
public class GridCreationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GridCreation gridCreation = (GridCreation)target;

        if (GUILayout.Button("Create Grid"))
        {
            gridCreation.BuildGrid(Handles.GetMainGameViewSize());
        }

        if (GUILayout.Button("Destory Grid"))
        {
            gridCreation.DeleteGrid();
        }
    }
}
