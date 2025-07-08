using UnityEngine;

public class PlayerRevive : MonoBehaviour
{
    private Vector3 positionRevive;
    private CharacterController controller;
    public bool useKill = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        positionRevive = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint")){
            positionRevive = other.transform.position;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Kill")){
            // if(!useKill) return;
            Debug.Log("colliion");
            Revive();
        }
    }

    public void Revive(){
        controller.enabled = false;
        transform.position = positionRevive;
        controller.enabled = true;
    }
}
