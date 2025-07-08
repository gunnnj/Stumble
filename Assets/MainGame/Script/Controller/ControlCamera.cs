using System.Collections;
using UnityEngine;

public class ControlCamera : MonoBehaviour
{
    [SerializeField] SwitchCamera switchCamera;
    private PlayerMovement player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
        // StartCoroutine(PlayCutScene());
    }
    public IEnumerator PlayCutScene(){
        player.canControl = false;
        yield return new WaitForSeconds(1f);
        switchCamera.SwitchCam(1);
        yield return new WaitForSeconds(2f);
        switchCamera.SwitchCam(2);
        player.canControl = true;
    }
}
