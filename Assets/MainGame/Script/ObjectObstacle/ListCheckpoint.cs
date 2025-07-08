using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListCheckpoint : MonoBehaviour
{
    [SerializeField] Checkpoint[] list;
    public static ListCheckpoint Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetIDCheckPoint();
    }

    public void SetIDCheckPoint(){
        for(int i=0; i<list.Count(); i++){
            list[i].IDcheckpoint = i;
        }
    }

    public Transform GetCheckpoint(int id){
        return list[id].transform;
    }

    public Transform GetPointNextCheckpoint(int id){
        if(id != list.Count()-1) return list[id+1].RandomPoint();
        else return null;
    }

}