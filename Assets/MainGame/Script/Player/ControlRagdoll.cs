using UnityEngine;

public class ControlRagdoll : MonoBehaviour
{
    public Rigidbody[] rigidbodies;
    public Animator animator;

    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        DisableRagdoll();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C)){
            EnableRagdoll();
        }
        if(Input.GetKeyDown(KeyCode.V)){
            DisableRagdoll();
        }
    }

    [ContextMenu("Dis")]
    public void DisableRagdoll(){
        animator.enabled =true;
        foreach(var item in rigidbodies){

            item.isKinematic = true;
        }
    }
    [ContextMenu("Enable")]
    public void EnableRagdoll(){
        animator.enabled =false;
        foreach(var item in rigidbodies){

            item.isKinematic = false;
        }
    }
}
