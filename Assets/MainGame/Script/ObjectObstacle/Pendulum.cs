using UnityEngine;

public class Pendulum : MonoBehaviour
{
    public float rotateSpeed = 50f; 
    public float targetAngle = 60f; 
    public bool isHorizontal = true;
    public bool isReverse = true;
    private float currentAngle = 0f; 
    private bool rotatingToTarget = true; 

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        float angleChange = rotateSpeed * Time.deltaTime;
        
        
        if(isReverse){
            currentAngle += rotatingToTarget ? angleChange : -angleChange;
        }
        else{
            currentAngle += rotatingToTarget ? -angleChange : angleChange;
        }

        currentAngle = Mathf.Clamp(currentAngle, -targetAngle, targetAngle);
        

        if(isHorizontal){
            transform.rotation = Quaternion.Euler(0,-90 , currentAngle);
        }
        else{
            transform.rotation = Quaternion.Euler( 0 , 0, currentAngle);
        }
        


        if (currentAngle >= targetAngle || currentAngle <= -targetAngle)
        {
            rotatingToTarget = !rotatingToTarget;
        }
    }
}
