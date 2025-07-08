using UnityEngine;

public class ControlerAnimBotAI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float checkDistance = 0.1f;
    // private AIPlayback aIPlayback;
    private MapReviveUI mapReviveUI;
    private const string AnimRun = "isRun";
    private const string AnimJum = "isJump";
    void Start()
    {
        mapReviveUI = FindFirstObjectByType<MapReviveUI>();
        // aIPlayback = GetComponent<AIPlayback>();
        animator.SetBool(AnimRun,true);
    }

    void Update()
    {
        
        if(IsGrounded()){
            animator.SetBool(AnimJum,false);
        }
        else{
            animator.SetBool(AnimJum,true);
        }
        // if(aIPlayback.LastIndex()){
        //     animator.SetBool(AnimRun,false);
        // }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
        // Vector3 checkPosition = transform.position - new Vector3(0, checkDistance, 0);
    
        // return Physics.CheckSphere(checkPosition, 0.25f, groundLayer);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Win")){
            Debug.Log("Goal");
            mapReviveUI.UpdateRank();
            // animator.SetBool(AnimRun,false);
        }
    }

}
