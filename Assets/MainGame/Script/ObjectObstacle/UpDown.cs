using System.Threading.Tasks;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    public float distance = 5f;
    public float speed = 3f;
    public float timeWait = 2f;
    Vector3 targetUp;
    Vector3 targetDown;
    public bool isUp = false;

    void Start()
    {
        targetUp = transform.position + Vector3.up*distance;
        targetDown = transform.position + Vector3.down*distance;
    }
    // void Update()
    // {
    //     if(isUp){
    //         transform.position = Vector3.MoveTowards(transform.position,targetUp,speed*Time.deltaTime);
    //         if(Vector3.Distance(transform.position,targetUp)<0.1f){
    //             isUp = false;
    //         }
    //     }
    //     else{
    //        transform.position = Vector3.MoveTowards(transform.position,targetDown,speed*Time.deltaTime);
    //         if(Vector3.Distance(transform.position,targetDown)<0.1f){
    //             isUp = true;
    //         } 
    //     }
    // }
    async void FixedUpdate()
    {
        if (isUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetUp, speed * Time.fixedDeltaTime);
            if (Vector3.Distance(transform.position, targetUp) < 0.1f)
            {
                await Task.Delay((int)(timeWait*1000));
                isUp = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetDown, speed * Time.fixedDeltaTime);
            if (Vector3.Distance(transform.position, targetDown) < 0.1f)
            {
                isUp = true;
            }
        }
    }
}
