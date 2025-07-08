using System.IO;
using UnityEngine;

public class AIPlayback : MonoBehaviour
{
    public int indexRecorder = 1;
    private InputDataList recordedData;
    private float timer = 0f;
    private int currentIndex = 0;
    private bool isInitialized = false;
    Vector3 firstPosition;
    // bool isStart = false;

    void Start()
    {
        // Đọc dữ liệu từ file
        string json = File.ReadAllText("D:/projects/ClimbingProject/Assets/MainGame/Recorder" + "/recorded_inputs"+indexRecorder+".json");
        recordedData = JsonUtility.FromJson<InputDataList>(json);

        if (recordedData != null && recordedData.inputs != null && recordedData.inputs.Count > 0)
        {
            // firstPosition = recordedData.inputs[0].position;
            // Debug.Log("Vị trí đầu tiên: " + firstPosition);

            transform.rotation = recordedData.inputs[0].rotation; 
            transform.rotation = recordedData.inputs[0].rotation;
            isInitialized = true;

            // (Tùy chọn) Gán vị trí đầu tiên cho transform của GameObject
            transform.position = firstPosition;
        }
        else
        {
            Debug.LogWarning("Danh sách inputs rỗng hoặc không đọc được file JSON.");
        }
    }

    void Update()
    {
        if (!isInitialized || recordedData == null || recordedData.inputs == null || recordedData.inputs.Count == 0)
            return;
        timer += Time.deltaTime;

        // Tìm bản ghi phù hợp với thời gian hiện tại
        // if (currentIndex < recordedData.inputs.Count && timer >= recordedData.inputs[currentIndex].time)
        // // if(!isStart)
        //     transform.position = Vector3.MoveTowards(transform.position,firstPosition,4f*Time.deltaTime);
        //     if(Vector3.Distance(transform.position,firstPosition)<0.1f){
        //         // isStart = true;
        //     }
            
        // if(currentIndex < recordedData.inputs.Count && isStart)
        if (currentIndex < recordedData.inputs.Count && timer >= recordedData.inputs[currentIndex].time)
        {
            var data = recordedData.inputs[currentIndex];
            // Áp dụng di chuyển
            Vector3 movement = new Vector3(data.moveX, 0, data.moveY) * Time.deltaTime * 5f;
            transform.Translate(movement);

            // Áp dụng xoay
            transform.Rotate(0, data.mouseX * Time.deltaTime * 100f, 0);

            // // Nhảy nếu cần
            // if (data.isJumping)
            //     GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);

            // Đồng bộ vị trí và xoay chính xác
            transform.position = data.position;
            transform.rotation = data.rotation;

            currentIndex++;
        }
    }
    public bool LastIndex(){
        return currentIndex == recordedData.inputs.Count-1;
    }
}
