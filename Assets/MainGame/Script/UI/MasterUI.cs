using System.Collections.Generic;
using UnityEngine;

public class MasterUI : MonoBehaviour
{
    public static MasterUI Instance {get; private set;}
    [SerializeField] protected List<ScreenUI> listScreen;

    private void Awake()
    {
        if(Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
    }

    public void HidenUI(){
        foreach(var item in listScreen){
            item.gameObject.SetActive(false);
        }
    }
    public void DisplayScreen(Screen screen){
        HidenUI();
        listScreen[(int) screen].gameObject.SetActive(true);
    }

    public enum Screen{
        Home,
        Custom,
        Map,
        Wheel
    }


}
