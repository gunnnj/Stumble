using UnityEngine;

public class Rolling : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float speed = 10f;
    public float distance = 5f;
    public bool isLeft = false;
    public bool isHoz = true;
    Vector3 targetL;
    Vector3 targetR;

    void Start()
    {
        if(isHoz){
            targetL = transform.position + Vector3.back*distance;
            targetR = transform.position + Vector3.forward*distance;
        }
        else{
            targetL = transform.position + Vector3.right*distance;
            targetR = transform.position + Vector3.left*distance;
        }
        
    }
    void Update()
    {
        if(isHoz){
            if(isLeft){
                transform.Rotate(Vector3.back, rotationSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position,targetL,speed*Time.deltaTime);
                if(Vector3.Distance(transform.position,targetL)<0.1f){
                    isLeft = false;
                }
            }
            else{
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position,targetR,speed*Time.deltaTime);
                if(Vector3.Distance(transform.position,targetR)<0.1f){
                    isLeft = true;
                }
            }
        }
        else{
            if(isLeft){
                transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position,targetL,speed*Time.deltaTime);
                if(Vector3.Distance(transform.position,targetL)<0.1f){
                    isLeft = false;
                }
            }
            else{
                transform.Rotate(Vector3.left, rotationSpeed * Time.deltaTime);
                transform.position = Vector3.MoveTowards(transform.position,targetR,speed*Time.deltaTime);
                if(Vector3.Distance(transform.position,targetR)<0.1f){
                    isLeft = true;
                }
            }
        }
        
        
    }
}
