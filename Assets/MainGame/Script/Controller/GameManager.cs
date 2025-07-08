using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public async void Play(){
        MasterUI.Instance.HidenUI();
        ManageScene.Instance.DisplayScene(ManageScene.Scene.FindPlayer);
        await Task.Delay(1000);
        MasterUI.Instance.HidenUI();
        ManageScene.Instance.Hiden();
        MasterUI.Instance.DisplayScreen(MasterUI.Screen.Map);
        await Task.Delay(6000);
        // SceneManager.LoadScene(1);
    }
    public void Custom(){
        ManageScene.Instance.DisplayScene(ManageScene.Scene.Custom);
        MasterUI.Instance.DisplayScreen(MasterUI.Screen.Custom);
    }
    public void Drop(){
        MasterUI.Instance.HidenUI();
        ManageScene.Instance.DisplayScene(ManageScene.Scene.Drop);
    }

    public void Home(){
        MasterUI.Instance.DisplayScreen(MasterUI.Screen.Home);
        ManageScene.Instance.DisplayScene(ManageScene.Scene.Home);
    }
    public void Wheel(){
        MasterUI.Instance.DisplayScreen(MasterUI.Screen.Wheel);
        ManageScene.Instance.Hiden();
    }
    public void PLayMapShooter(){
        SceneManager.LoadScene(5);
    }
    public void PlayMapBlock(){
        SceneManager.LoadScene(4);
    }
    public void PlayMapWater(){
        SceneManager.LoadScene(3);
    }
    public void PlayMapBee(){
        SceneManager.LoadScene(2);
    }
    public void PlayMapGoal(){
        SceneManager.LoadScene(1);
    }
    
}
