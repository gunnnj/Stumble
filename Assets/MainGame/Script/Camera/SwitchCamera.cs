using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public List<CinemachineCamera> cameras;
    private CinemachineCamera currentCamera;


    public void SwitchCam(int indexCam)
    {
        currentCamera.Priority = 0;
        cameras[indexCam].Priority = 10;
        currentCamera = cameras[indexCam];
    }
    private void Start()
    {
        foreach (var cam in cameras)
        {
            cam.Priority = 0;
        }
        currentCamera = cameras[0];
        cameras[0].Priority = 10;
    }


}
