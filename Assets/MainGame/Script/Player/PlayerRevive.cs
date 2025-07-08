using UnityEngine;

public class PlayerRevive : MonoBehaviour
{
    private Vector3 positionRevive;
    private CharacterController controller;

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

    public void Revive(){
        controller.enabled = false;
        transform.position = positionRevive;
        controller.enabled = true;
    }
}
