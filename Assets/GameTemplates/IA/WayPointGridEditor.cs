using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayPointGrid))]
public class WayPointGridEditor : Editor
{
    static float WayPointDistance = 2.0f;
    static int GridSize = 4;//64;



    /**
     * O que tem no waypointgrid do professor
     * Editor Style = VERTEX_EDGE (Como é mostrado no editor - não importante agora)
     * Layer Mask Colision Mixed (mascara de colizão que tem vários objetos distintos SEM SER TERRENO)
     * Layer Mask  Update Height Terrain (mascara de colizão que tem TERRENO)
     * Way point Height Off Set 0.25
     * Way point Prefab Way Point (pro script de waypont)
     * 
     * 
     * 
     * 
     */




    //SerializedProperty AttributeExample;

    static WayPointGrid WaypointGrid;

    public override void OnInspectorGUI()
    {
        Rect rect = EditorGUILayout.GetControlRect(false, 5.0f);
        rect.height = 5.0f;

        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1.0f));

        rect.y += 80.0f;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1.0f));

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        WayPointDistance = EditorGUILayout.FloatField("Distance to waypoint", WayPointDistance);

        GridSize = EditorGUILayout.IntSlider("Size of the grid", GridSize, 0, 256);

        if (GUILayout.Button("Create Grid"))
            CreateGrid();

        EditorGUILayout.Space();

        rect.y += 30.0f;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1.0f));

        if (GUILayout.Button("TESTE // Load Grid"))
            LoadGrid();

        rect.y += 30.0f;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1.0f));

        EditorGUILayout.Space();

        if (GUILayout.Button("Update Grid Vertices (Collision and Height)"))
            UpdateGrid();

        EditorGUILayout.Space();

        if (GUILayout.Button("Print Grid"))
            PrintGrid();

        rect.y += 30.0f;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1.0f));
        EditorGUILayout.Space();

        //AttributeExample = serializedObject.FindProperty("AttributeExample");

        //serializedObject.Update();
        //EditorGUILayout.PropertyField(AttributeExample);
        //serializedObject.ApplyModifiedProperties();
    }

    void CreateGrid()
    {
        //WayPointGrid waypoint = (WayPointGrid) target;
        WaypointGrid = (WayPointGrid) target;
        WaypointGrid.CreateWaypointGrid(GridSize, WayPointDistance);
        Debug.Log("Grid created!");
    }

    void UpdateGrid()
    {
        // TODO Update Grid Collision and Height
    }

    void LoadGrid()
    {
        // TODO Load the grid data from GameObjects
        WaypointGrid.Teste();
    }


    void PrintGrid()
    {
        WaypointGrid.printGrid();
    }
}
