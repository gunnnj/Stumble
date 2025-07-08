using UnityEngine;
using System.IO;
using UnityEditor;
using System.Collections.Generic;

public class JsonToScriptableObject : MonoBehaviour
{
    [System.Serializable]
    public class InputDataList
    {
        public List<InputData> inputs;
    }

    // Chuyển từng file JSON thành một ScriptableObject riêng
    public void ConvertJsonListToScriptableObjects(string jsonFolderPath, string assetFolderPath)
    {
        try
        {
            #if UNITY_EDITOR
            if (!AssetDatabase.IsValidFolder("Assets/" + assetFolderPath))
            {
                AssetDatabase.CreateFolder("Assets", assetFolderPath);
            }
            #endif

            string[] jsonFiles = Directory.GetFiles(jsonFolderPath, "*.json");
            if (jsonFiles.Length == 0)
            {
                Debug.LogWarning("Không tìm thấy file JSON nào trong: " + jsonFolderPath);
                return;
            }

            foreach (string jsonFile in jsonFiles)
            {
                string json = File.ReadAllText(jsonFile);
                InputDataList jsonData = JsonUtility.FromJson<InputDataList>(json);

                if (jsonData == null || jsonData.inputs == null || jsonData.inputs.Count == 0)
                {
                    Debug.LogWarning("Dữ liệu JSON rỗng trong file: " + jsonFile);
                    continue;
                }

                InputDataAsset dataAsset = ScriptableObject.CreateInstance<InputDataAsset>();
                dataAsset.inputs = jsonData.inputs;

                string fileName = Path.GetFileNameWithoutExtension(jsonFile);
                string assetPath = $"Assets/{assetFolderPath}/{fileName}.asset";

                #if UNITY_EDITOR
                AssetDatabase.CreateAsset(dataAsset, assetPath);
                Debug.Log("Đã tạo ScriptableObject tại: " + assetPath);
                #endif
            }

            #if UNITY_EDITOR
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Hoàn tất chuyển đổi JSON sang ScriptableObject.");
            #endif
        }
        catch (System.Exception e)
        {
            Debug.LogError("Lỗi khi chuyển JSON: " + e.Message);
        }
    }

    // // (Tùy chọn) Gộp tất cả JSON vào một ScriptableObject
    // public void ConvertJsonListToSingleScriptableObject(string jsonFolderPath, string assetPath)
    // {
    //     try
    //     {
    //         InputDataAsset combinedDataAsset = ScriptableObject.CreateInstance<InputDataAsset>();
    //         combinedDataAsset.inputs = new List<InputData>();

    //         string[] jsonFiles = Directory.GetFiles(jsonFolderPath, "*.json");
    //         if (jsonFiles.Length == 0)
    //         {
    //             Debug.LogWarning("Không tìm thấy file JSON nào trong: " + jsonFolderPath);
    //             return;
    //         }

    //         foreach (string jsonFile in jsonFiles)
    //         {
    //             string json = File.ReadAllText(jsonFile);
    //             InputDataList jsonData = JsonUtility.FromJson<InputDataList>(json);

    //             if (jsonData != null && jsonData.inputs != null && jsonData.inputs.Count > 0)
    //             {
    //                 combinedDataAsset.inputs.AddRange(jsonData.inputs);
    //             }
    //         }

    //         #if UNITY_EDITOR
    //         string assetFullPath = "Assets/" + assetPath + ".asset";
    //         AssetDatabase.CreateAsset(combinedDataAsset, assetFullPath);
    //         AssetDatabase.SaveAssets();
    //         AssetDatabase.Refresh();
    //         Debug.Log("Đã tạo ScriptableObject gộp tại: " + assetFullPath);
    //         #endif
    //     }
    //     catch (System.Exception e)
    //     {
    //         Debug.LogError("Lỗi khi gộp JSON: " + e.Message);
    //     }
    // }

    // Hàm gọi ví dụ
    [ContextMenu("Convert")]
    public void ConvertAndSaveAll()
    {
        string jsonFolderPath = Path.Combine(Application.dataPath, "MainGame/Recorder");
        string assetFolderPath = "MainGame/FileAsset";
        ConvertJsonListToScriptableObjects(jsonFolderPath, assetFolderPath);
    }
    
}
