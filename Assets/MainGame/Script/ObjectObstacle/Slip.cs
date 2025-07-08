using UnityEngine;

public class Slip : MonoBehaviour
{
    [SerializeField] float force;
    public bool setForPlayer = false;
    public bool forwardDir = true;
    public bool rightDir = false;
    public bool reverse = true;
    private Rigidbody _rb;
    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            _rb = other.gameObject.GetComponent<Rigidbody>();
            if(setForPlayer){
                _rb.AddForce(other.transform.forward*force,ForceMode.Impulse);
            }
            else{
                if(forwardDir){
                    if(reverse){
                        _rb.AddForce(-transform.forward*force,ForceMode.Impulse);
                        return;
                    } 
                    _rb.AddForce(transform.forward*force,ForceMode.Impulse);
                    return;
                }
                if(rightDir){
                    if(reverse){
                        _rb.AddForce(-transform.right*force,ForceMode.Impulse);
                        return;
                    } 
                    _rb.AddForce(transform.right*force,ForceMode.Impulse);
                    return;
                }

            }
        }
    }
}
