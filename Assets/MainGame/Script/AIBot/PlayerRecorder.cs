using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour
{
    [SerializeField] int indexRecorder = 2; 
    private List<InputData> recordedInputs = new List<InputData>();
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        InputData data = new InputData
        {
            time = timer,
            moveX = Input.GetAxis("Horizontal"),
            moveY = Input.GetAxis("Vertical"),
            // mouseX = Input.GetAxis("Mouse X"),
            isJumping = Input.GetKey(KeyCode.Space),
            position = transform.position,
            rotation = transform.rotation
        };
        recordedInputs.Add(data);
    }

    // void OnApplicationQuit()
    // {
    //     // Lưu dữ liệu vào file JSON
    //     string json = JsonUtility.ToJson(new InputDataList { inputs = recordedInputs });
    //     File.WriteAllText(Application.dataPath + "/recorded_inputs.json", json);
    //     Debug.Log("Save to path: "+Application.dataPath + "/recorded_inputs.json");
    // }
    void OnDestroy()
    {
        // Lưu dữ liệu vào file JSON
        string json = JsonUtility.ToJson(new InputDataList { inputs = recordedInputs });
        File.WriteAllText("D:/projects/ClimbingProject/Assets/MainGame/Recorder" + "/recorded_inputs"+indexRecorder+".json", json);
        Debug.Log("Save to path: "+"D:/projects/ClimbingProject/Assets/MainGame/Recorder" + "/recorded_inputs"+indexRecorder+".json");
    }
}

// [System.Serializable]
// public struct InputData
// {
//     public float time;
//     public float moveX, moveY, mouseX;
//     public bool isJumping;
//     public Vector3 position;
//     public Quaternion rotation;
// }

[System.Serializable]
public class InputDataList
{
    public List<InputData> inputs;
}

