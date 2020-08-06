using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneIDMapEraser))]
public class SceneIDMapEraserEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if(GUILayout.Button("Find and Kill SceneIDMap"))
        {
            Selection.activeGameObject = GameObject.Find("SceneIDMap");
            DestroyImmediate(Selection.activeObject);
        }
    }
}
