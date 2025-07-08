using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PrefabCreator))]
public class EditorPrefab : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        PrefabCreator prefabCreator = (PrefabCreator)target;

        if (GUILayout.Button("Prev"))
        {
            prefabCreator.PrevPrefab();
        }
        if (GUILayout.Button("Next"))
        {
            prefabCreator.NextPrefab();

        }
        if (GUILayout.Button("RotationX"))
        {
            prefabCreator.RotationX();
        }
        if (GUILayout.Button("RotationY"))
        {
            prefabCreator.RotationY();
        }
        if (GUILayout.Button("RotationZ"))
        {
            prefabCreator.RotationZ();
        }

        if (GUILayout.Button("Save"))
        {
            prefabCreator.Save();
        }
    }

}