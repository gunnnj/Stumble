using UnityEngine;

public class BotBlock : MonoBehaviour
{
    private Rigidbody rb;
    public AnimationCurve jumpCurve;
    public float jumpHeight = 2.0f;
    public float jumpDuration = 1.0f;
    public Transform target;

    private bool isJumping = false;
    private float jumpStartTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            StartJump();
        }

        if (isJumping)
        {
            PerformJump();
        }
    }

    void StartJump()
    {
        isJumping = true;
        jumpStartTime = Time.time;
    }

    void PerformJump()
    {
        float elapsed = (Time.time - jumpStartTime) / jumpDuration;
        float yOffset = jumpCurve.Evaluate(elapsed) * jumpHeight;

        if (elapsed >= 1f)
        {
            isJumping = false; 
            yOffset = 0; 
        }

        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);
        rb.MovePosition(newPosition);
    }
}
