using DiasGames.Controller;
using UnityEngine;

public class CheckBounce : MonoBehaviour
{
    [SerializeField] GameObject player;
    public PlayerRevive playerRevive;
    private CSPlayerController cSPlayer;
    Transform oldParent;
    void Start()
    {
        cSPlayer = FindFirstObjectByType<CSPlayerController>();
        playerRevive = player.GetComponent<PlayerRevive>();
        oldParent = player.transform.parent;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Parent")){
            player.transform.parent = other.transform;
        }
        if(other.CompareTag("Kill")){
            Debug.Log("Trigger");
            playerRevive.Revive();
        }
        if(other.CompareTag("Bounce")){
            cSPlayer.OnBounce(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Parent"))
        {
            player.transform.parent = oldParent;
        }
        if(other.CompareTag("Bounce")){
            cSPlayer.OnBounce(false);
        }
    }


    

}
