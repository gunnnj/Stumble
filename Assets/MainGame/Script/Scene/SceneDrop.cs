using System.Collections.Generic;
using UnityEngine;

public class SceneDrop : BaseScene
{
    [SerializeField] int index;
    [SerializeField] Transform cushionPlayer;
    public List<Rigidbody> listCushion = new List<Rigidbody>();

    void Start()
    {
        AddCushion();
    }
    public void AddCushion(){
        for(int i=0; i<cushionPlayer.childCount; i++){
            listCushion.Add(cushionPlayer.GetChild(i).GetComponent<Rigidbody>());
        }
    }
    [ContextMenu("Drop")]
    public void Drop(){
        listCushion[index].useGravity = true;
        
    }
}
