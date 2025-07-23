using DiasGames.Controller;
using DiasGames.Mobile;
using UnityEngine;

public class ChangeStateButton : MonoBehaviour
{
    public MobileButton mobileButton;
    public CSPlayerController playerController;

    void Start()
    {
        mobileButton = GetComponent<MobileButton>();

    }

    void Update()
    {
        if(playerController._mover.IsGrounded()){
            mobileButton.Button = MobileButton.InputButton.Jump;
        }
        else{
            mobileButton.Button = MobileButton.InputButton.Drop;
        }
    }
}
