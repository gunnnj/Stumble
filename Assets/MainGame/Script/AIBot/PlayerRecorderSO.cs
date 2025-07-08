using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;

public class PlayerRecorderSO : MonoBehaviour
{
    [SerializeField] int index = 5;
    private List<InputData> recordedInputs = new List<InputData>();
    private InputData lastData;
    private float timer = 0f;
    private bool isRecording = false;

    // Đường dẫn thư mục lưu ScriptableObject trong Editor
    [SerializeField] private string assetFolderPath = "Resources/InputDataAssets";

    void Start()
    {
        // Bắt đầu ghi ngay khi khởi động
        StartRecording();
    }

    void Update()
    {
        if (!isRecording)
            return;

        timer += Time.deltaTime;
        InputData data = new InputData
        {
            time = timer,
            moveX = Input.GetAxis("Horizontal"),
            moveY = Input.GetAxis("Vertical"),
            mouseX = Input.GetAxis("Mouse X"),
            isJumping = Input.GetKey(KeyCode.Space),
            position = transform.position,
            rotation = transform.rotation
        };

        // Chỉ lưu nếu dữ liệu thay đổi đáng kể
        if (!IsDataSimilar(data, lastData))
        {
            recordedInputs.Add(data);
            lastData = data;
        }
    }

    // Kiểm tra dữ liệu có tương tự để tránh lưu trùng
    private bool IsDataSimilar(InputData newData, InputData oldData)
    {
        const float threshold = 0.01f;
        return Mathf.Abs(newData.moveX - oldData.moveX) < threshold &&
               Mathf.Abs(newData.moveY - oldData.moveY) < threshold &&
               Mathf.Abs(newData.mouseX - oldData.mouseX) < threshold &&
               newData.isJumping == oldData.isJumping &&
               Vector3.Distance(newData.position, oldData.position) < threshold &&
               Quaternion.Angle(newData.rotation, oldData.rotation) < threshold;
    }

    // Lưu dữ liệu vào một ScriptableObject mới
    public void SaveToNewScriptableObject()
    {
        if (recordedInputs.Count == 0)
        {
            Debug.LogWarning("Không có dữ liệu để lưu.");
            return;
        }

        // Tạo instance ScriptableObject mới
        InputDataAsset dataAsset = ScriptableObject.CreateInstance<InputDataAsset>();
        dataAsset.inputs = new List<InputData>(recordedInputs);

        // Lưu ScriptableObject thành file .asset với tên duy nhất
        #if UNITY_EDITOR
        try
        {
            // Tạo thư mục nếu chưa tồn tại
            string fullFolderPath = "Assets/" + assetFolderPath;
            if (!Directory.Exists(fullFolderPath))
            {
                Directory.CreateDirectory(fullFolderPath);
            }

            // Tạo tên file duy nhất dựa trên timestamp
            string timestamp = System.DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string assetPath = $"{fullFolderPath}/Recorded_inputs {index}.asset";

            // Lưu ScriptableObject
            AssetDatabase.CreateAsset(dataAsset, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("Đã tạo ScriptableObject mới tại: " + assetPath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Lỗi khi lưu ScriptableObject: " + e.Message);
        }
        #else
        Debug.Log("Dữ liệu đã được lưu vào ScriptableObject trong runtime: " + dataAsset.name);
        #endif
    }

    // Gọi khi dừng ghi (ví dụ: khi dừng Play Mode hoặc thoát game)
    void OnDestroy()
    {
        #if UNITY_EDITOR
        SaveToNewScriptableObject();
        #endif
    }

    // Hàm để bắt đầu/dừng ghi
    public void StartRecording()
    {
        isRecording = true;
        recordedInputs.Clear();
        timer = 0f;
        Debug.Log("Bắt đầu ghi dữ liệu.");
    }

    public void StopRecording()
    {
        isRecording = false;
        SaveToNewScriptableObject();
        Debug.Log("Dừng ghi dữ liệu.");
    }
}
