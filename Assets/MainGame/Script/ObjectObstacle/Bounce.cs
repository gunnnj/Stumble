using System.Collections;
using DiasGames.Components;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float pushDistance = 2f; // Khoảng cách đẩy
    private float pushSpeed = 10f;   // Tốc độ đẩy
    // [SerializeField] private bool useObjectUpDirection = true; // Đẩy theo transform.up của object
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            Debug.Log("Bounce");
            IMover mover = other.gameObject.GetComponent<IMover>();
            if (mover != null)
            {
                // Tính toán hướng đẩy
                Vector3 pushDirection =  -other.transform.forward;

                // Tính toán vận tốc đẩy
                Vector3 pushVelocity = pushDirection.normalized * pushSpeed;

                // Áp dụng vận tốc đẩy cho player
                mover.SetVelocity(pushVelocity);

                // Tắt trọng lực tạm thời để tạo cảm giác đẩy mượt mà
                mover.DisableGravity();

                // Bật lại trọng lực sau một khoảng thời gian (dựa trên pushDistance và pushSpeed)
                StartCoroutine(EnableGravityAfterDelay(mover, pushDistance / pushSpeed));

            }
        }
    }
    public IEnumerator EnableGravityAfterDelay(IMover mover, float delay)
    {
        yield return new WaitForSeconds(delay);
        mover.EnableGravity();
    }
}
