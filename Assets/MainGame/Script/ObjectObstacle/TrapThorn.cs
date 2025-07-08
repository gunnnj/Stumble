using DG.Tweening;
using UnityEngine;

public class TrapThorn : MonoBehaviour
{
    public float moveDistance = 2f; 
    public float duration = 1f;

    void Start()
    {
        KnobThorn();
    }

    public void KnobThorn(){
        transform.DOMoveY(transform.position.y - moveDistance, 0.2f)
            .SetEase(Ease.Linear) // Cài đặt easing nếu cần
            .OnComplete(() => 
            {
                // Di chuyển xuống về vị trí ban đầu
                transform.DOMoveY(transform.position.y + moveDistance, 0.2f)
                    .SetEase(Ease.Linear)
                    .OnComplete(() => 
                    {
                        // Chờ 2 giây trước khi lặp lại
                        DOVirtual.DelayedCall(1f, KnobThorn);
                    });
            });
    }
}
