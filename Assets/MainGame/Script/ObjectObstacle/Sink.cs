using UnityEngine;

public class Sink : MonoBehaviour
{
    [SerializeField] float decreaseRate = 1f;
    private bool isLoop = true;
    private Vector3 originPos;
    

    void Start()
    {
        originPos = transform.position;
    }
    void Update()
    {
        if(isLoop){
            transform.position = Vector3.Lerp(transform.position,originPos,4f*Time.deltaTime);
        }
    }
    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            isLoop = false;
            Vector3 newPosition = transform.position;
            newPosition.y -= decreaseRate * Time.deltaTime;
            transform.position = newPosition;
        }
    }
    void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            isLoop = true;
        }
    }
}
