using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public void TeleportMap1(){
        SceneManager.LoadScene(0);
    }
    public void TeleportMap2(){
        SceneManager.LoadScene(1);
    }
}
