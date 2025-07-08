using System.Collections;
using DiasGames.Controller;
using UnityEngine;

public class ObjectParent : MonoBehaviour
{
    public CSPlayerController cSPlayer;

    void Start()
    {
        cSPlayer = FindFirstObjectByType<CSPlayerController>();
    }
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player")){
            // cSPlayer._mover.SetVelocity()
        }
    }
}
