using UnityEngine;

public class ManageScene : MonoBehaviour
{
    [SerializeField] GameObject[] listScene;

    public static ManageScene Instance;

    void Awake()
    {
        Instance = this;
    }

    public void Hiden(){
        foreach(var item in listScene){
            item.SetActive(false);
        }
    }
    public void DisplayScene(Scene scene){
        Hiden();
        listScene[(int)scene].SetActive(true);
    }

    public enum Scene{
        Home,
        Drop,
        FindPlayer,
        Custom
    }
}
