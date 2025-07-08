using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Mycomponent))]
public class MycomponentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Mycomponent myComponent = (Mycomponent)target;

        // Hiển thị trường message
        myComponent.message = EditorGUILayout.TextField("Message", myComponent.message);

        // Tạo nút
        if (GUILayout.Button("Show Message"))
        {
            EditorUtility.DisplayDialog("Message", myComponent.message, "OK");
        }

        // Gọi phương thức base để hiển thị các trường mặc định
        DrawDefaultInspector();
    }
}
