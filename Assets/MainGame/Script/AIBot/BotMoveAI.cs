using UnityEngine;

public class BotMoveAI : MonoBehaviour
{
    float speed = 4f;
    Vector3 dir;
    void Update()
    {
        float dirX = Input.GetAxis("Horizontal");
        float dirZ = Input.GetAxis("Vertical");
        dir = new Vector3(dirX,0,dirZ);
        if(dir.magnitude>0.1f){
            Move();
        }
        if(Input.GetKeyDown(KeyCode.Space)){
            GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }
    }
    private void Move()
    {
        Vector3 inputDirection = dir.normalized;
        Vector3 moveDirection = transform.TransformDirection(inputDirection);
        transform.position += moveDirection * speed * Time.deltaTime;
    }
}
