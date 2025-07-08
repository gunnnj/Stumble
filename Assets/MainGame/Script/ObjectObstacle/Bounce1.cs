using DG.Tweening;
using UnityEngine;

public class Bounce1 : MonoBehaviour
{
    private void Start()
    {
        // ScaleUpAndDown();
    }

    private void ScaleUpAndDown()
    {
        // Scale từ 0.2 đến 0.6 theo trục Y trong 0.5 giây
        transform.localScale = new Vector3(1f, 0.2f, 1f); // Đặt kích thước ban đầu (X và Z giữ nguyên)
        transform.DOScale(new Vector3(1f, 0.6f, 1f), 0.2f)
            .OnComplete(() => 
            {
                // Quay lại kích thước ban đầu
                transform.DOScale(new Vector3(1f, 0.2f, 1f), 0.3f);
            });
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            ScaleUpAndDown();
        }
    }
}
