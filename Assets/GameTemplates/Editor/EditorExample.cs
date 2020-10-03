using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
public class EditorExample : EditorWindow
{
    static float TestValue = 1.0f;

    [MenuItem("Window/Execute Test Value...")]
    static void Init()
    {
        EditorWindow.GetWindow<EditorExample>().Show();
    }

    [MenuItem("Window/Execute Test Value 2...")]
    static void Start()
    {
        Debug.Log("Selected Value: " + TestValue.ToString());
    }

    public void OnGUI()
    {
        TestValue = EditorGUILayout.FloatField("Value", TestValue);

        if (GUILayout.Button("Execute"))
            Execute();
    }

    void Execute()
    {
        Debug.Log("Selected Value: " + TestValue.ToString());
    }
}

