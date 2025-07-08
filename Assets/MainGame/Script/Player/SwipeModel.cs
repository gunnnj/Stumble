using UnityEngine;

public class SwipeModel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 200f;
    private Vector2 touchStartPos;
    private bool isSwiping = false;

    void Update()
    {
        // Phát hiện chạm
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Bắt đầu chạm, lưu vị trí
                    touchStartPos = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    // Tính delta khi vuốt
                    if (isSwiping)
                    {
                        Vector2 touchDelta = touch.position - touchStartPos;
                        float rotationY = touchDelta.x * rotationSpeed * Time.deltaTime;
                        transform.Rotate(0, -rotationY, 0); // Xoay quanh trục Y
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    // Kết thúc vuốt, reset
                    isSwiping = false;
                    break;
            }
        }
    }
}
