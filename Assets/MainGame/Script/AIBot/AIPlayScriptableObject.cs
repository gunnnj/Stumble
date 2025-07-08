using UnityEngine;

public class AIPlayScriptableObject : MonoBehaviour
{
    [SerializeField] private InputDataAsset dataAsset; // ScriptableObject duy nhất
    private float timer = 0f;
    private int currentIndex = 0;
    private bool isInitialized = false;

    void Start()
    {
        // // Tải ScriptableObject từ Resources (nếu chưa gán trong Inspector)
        // if (dataAsset == null)
        // {
        //     dataAsset = Resources.Load<InputDataAsset>("InputDataAssets/CombinedInputData");
        // }

        // Kiểm tra dữ liệu hợp lệ và gán vị trí đầu tiên
        if (dataAsset != null && dataAsset.inputs != null && dataAsset.inputs.Count > 0)
        {
            transform.position = dataAsset.inputs[0].position;
            transform.rotation = dataAsset.inputs[0].rotation;
            // Debug.Log("Đã đặt nhân vật tại vị trí đầu tiên: " + transform.position);
            isInitialized = true;
        }
        else
        {
            // Debug.LogWarning("Dữ liệu ScriptableObject rỗng hoặc không tải được.");
        }
    }
    void Update()
    {
        if (!isInitialized || dataAsset == null || dataAsset.inputs == null || dataAsset.inputs.Count == 0)
            return;

        timer += Time.deltaTime;

        // if (currentIndex < dataAsset.inputs.Count && timer >= dataAsset.inputs[currentIndex].time)
        // {
        //     var data = dataAsset.inputs[currentIndex];
        //     Vector3 movement = new Vector3(data.moveX, 0, data.moveY) * Time.deltaTime * 5f;
        //     transform.Translate(movement);
        //     transform.Rotate(0, data.mouseX * Time.deltaTime * 100f, 0);
        //     transform.position = data.position;
        //     transform.rotation = data.rotation;
        //     currentIndex++;
        // }
        while (currentIndex < dataAsset.inputs.Count && timer >= dataAsset.inputs[currentIndex].time)
        {
            var data = dataAsset.inputs[currentIndex];
            transform.position = data.position; // Chỉ cần gán vị trí
            transform.rotation = data.rotation; // Chỉ cần gán xoay
            currentIndex++;
        }
    }
}
