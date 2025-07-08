using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    public Transform target;
    public float speed = 10f;
    Vector3 originPos;
    public bool isTarget = false;
    public bool isLoop = true;

    void Start()
    {
        originPos = transform.position;
    }
    // void Update()
    // {
    //     if(!isTarget){
    //         transform.position = Vector3.MoveTowards(transform.position,target.position,speed*Time.deltaTime);
    //         if(Vector3.Distance(transform.position,target.position)<0.1f){
    //             isTarget = true;
    //         }
    //     }
    //     else{
    //         if(isLoop){
    //             transform.position = Vector3.MoveTowards(transform.position,originPos,speed*Time.deltaTime);
    //             if(Vector3.Distance(transform.position,originPos)<0.1f){
    //                 isTarget = false;
    //             }
    //         }
    //         else{
    //             if(Vector3.Distance(transform.position,target.position)<0.1f){
    //                 gameObject.SetActive(false);
    //             }
    //         }
    //     }
        
    // }
    void FixedUpdate()
    {
        if (!isTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                isTarget = true;
            }
        }
        else
        {
            if (isLoop)
            {
                transform.position = Vector3.MoveTowards(transform.position, originPos, speed * Time.fixedDeltaTime);
                if (Vector3.Distance(transform.position, originPos) < 0.1f)
                {
                    isTarget = false;
                }
            }
            else
            {
                if (Vector3.Distance(transform.position, target.position) < 0.1f)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

}
