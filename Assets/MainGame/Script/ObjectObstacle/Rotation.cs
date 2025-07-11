using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public bool isUp = true;
    public bool isRight = false;
    public bool isForward = false;
    public bool isReverse = false;
    // void Update()
    // {
    //     if(isUp){
    //         if(isReverse){
    //             transform.Rotate(-Vector3.up, rotationSpeed * Time.deltaTime);
    //             return;
    //         }
    //         transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    //         return;
    //     }
    //     if(isRight){
    //         if(isReverse){
    //             transform.Rotate(-Vector3.right, rotationSpeed * Time.deltaTime);
    //             return;
    //         }
    //         transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    //         return;
    //     }
    //     if(isForward){
    //         if(isReverse){
    //             transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
    //             return;
    //         }
    //         transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    //         return;
    //     }
        
    // }
    void FixedUpdate()
    {
        if(isUp){
            if(isReverse){
                transform.Rotate(-Vector3.up, rotationSpeed * Time.fixedDeltaTime);
                return;
            }
            transform.Rotate(Vector3.up, rotationSpeed * Time.fixedDeltaTime);
            return;
        }
        if(isRight){
            if(isReverse){
                transform.Rotate(-Vector3.right, rotationSpeed * Time.fixedDeltaTime);
                return;
            }
            transform.Rotate(Vector3.right, rotationSpeed * Time.fixedDeltaTime);
            return;
        }
        if(isForward){
            if(isReverse){
                transform.Rotate(-Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
                return;
            }
            transform.Rotate(Vector3.forward, rotationSpeed * Time.fixedDeltaTime);
            return;
        }
    }
}
