using UnityEngine;

public class BotBee : MonoBehaviour
{
    public float distanceToVertices = 2f; // Khoảng cách từ tâm đến đỉnh
    public float rayLength = 3f; // Chiều dài của tia ray
    public LayerMask layerMask; // Layer mask để kiểm tra
    public float rotationOffset = 30f; // Góc xoay thêm
    public float speed = 4f;
    Vector3 positionMove;
    void Start()
    {
        positionMove = Vector3.zero;
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void CastHexagonalRaycasts()
    {
        Vector3[] directions = new Vector3[6];
        float angle = 60f; // Mỗi góc giữa các tia là 60 độ

        // for (int i = 0; i < 6; i++)
        // {
        //     // Tính toán hướng theo góc, cộng thêm góc xoay
        //     float radian = Mathf.Deg2Rad * (angle * i + rotationOffset);
        //     Vector3 vertexPosition = transform.position + new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian)) * distanceToVertices;

        //     // Bắn raycast xuống dưới từ đỉnh
        //     RaycastHit hit;
        //     Vector3 rayEnd = vertexPosition + Vector3.down * rayLength;

        //     // Vẽ ray
        //     Debug.DrawLine(vertexPosition, rayEnd, Color.red);

        //     if (Physics.Raycast(vertexPosition, Vector3.down, out hit, rayLength, layerMask))
        //     {
        //         // Debug.Log($"Hit: {hit.collider.name} at distance: {hit.distance}");
        //         positionMove = hit.point;
        //         break;
        //     }
        // }

        int randomIndex;
        int attempts = 0; 

        while (attempts < 6)
        {
            randomIndex = Random.Range(0, 6); // Chọn ngẫu nhiên i từ 0 đến 5
            float radian = Mathf.Deg2Rad * (angle * randomIndex + 30f); // 30 độ xoay thêm
            Vector3 vertexPosition = transform.position + new Vector3(Mathf.Cos(radian), 0, Mathf.Sin(radian)) * distanceToVertices;

            // Bắn raycast xuống dưới từ đỉnh
            RaycastHit hit;
            Vector3 rayEnd = vertexPosition + Vector3.down * rayLength;

            // Vẽ ray
            Debug.DrawLine(vertexPosition, rayEnd, Color.red);

            if (Physics.Raycast(vertexPosition, Vector3.down, out hit, rayLength, layerMask))
            {
                positionMove = hit.point; // Lưu vị trí chạm
                break; // Thoát khỏi vòng lặp nếu có va chạm
            }

            attempts++; // Tăng số lần thử
        }
    }
    public void MoveToTarget(){
        if(Vector3.Distance(transform.position,positionMove)<0.1f || Vector3.Distance(transform.position,positionMove)>5f){
            CastHexagonalRaycasts();
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position,positionMove,speed*Time.deltaTime);
        }
    }
}
