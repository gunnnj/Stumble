using DG.Tweening;
using UnityEngine;

public class Pan : MonoBehaviour
{
    public float jumpForce = 10f;
    public GameObject go;
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            ScaleEffect(other);
            
        }
    }
    
    private void ScaleEffect(Collider other)
    {
        // Lưu kích thước ban đầu
        Vector3 originalScale = go.transform.localScale;
        Vector3 targetScale = new Vector3(originalScale.x, 0.8f, originalScale.z); // Kích thước mục tiêu

        // Giảm scale xuống 0.8
        go.transform.DOScale(targetScale, 0.1f).OnComplete(() =>
        {
            // Tăng scale trở lại 1
            go.transform.DOScale(originalScale, 0.1f);
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);

            Debug.Log("nảy bật");
        });
    }
}
