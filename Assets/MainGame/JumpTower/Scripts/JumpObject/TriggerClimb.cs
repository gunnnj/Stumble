using DiasGames.Controller;
using UnityEngine;

public class TriggerClimb : MonoBehaviour
{
    private CSPlayerController cSPlayer;

    void Start()
    {
        cSPlayer = FindFirstObjectByType<CSPlayerController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            cSPlayer.OnJump(true);
        }
    }
}
