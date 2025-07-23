using UnityEngine;

public class FloatingPet : MonoBehaviour
{
    [Header("Floating Settings")]
    public Transform target;
    public float floatHeight = 0.5f; // Chiều cao lơ lửng
    public float floatSpeed = 2f; // Tốc độ lơ lửng

    private Vector3 originalPosition;

    void Start()
    {
        // Lưu vị trí ban đầu
        originalPosition = transform.position;
    }

    void Update()
    {
        // Tạo hiệu ứng lơ lửng
        // float newY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;


        // // Cập nhật vị trí của cầu
        // transform.position = new Vector3(transform.position.x , transform.position.y+newY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position,target.position,Time.deltaTime*floatSpeed);
        Vector3 lookDir = target.position - transform.position;
        lookDir.y = 0;
    
        if(lookDir.magnitude >0.1f){
            Quaternion quaternion = Quaternion.LookRotation(lookDir);
            transform.rotation = quaternion;
        }
        
    }
}
