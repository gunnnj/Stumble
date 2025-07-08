using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int IDcheckpoint;
    public List<Transform> listPoint = new List<Transform>();

    void Start()
    {
        AddPointToList();
    }
    public void AddPointToList(){
        for(int i=0; i<transform.childCount; i++){
            listPoint.Add(transform.GetChild(i));
        }
    }
    public Transform RandomPoint(){
        Transform point  = listPoint[Random.Range(0,listPoint.Count-1)];
        return point;
    }
    public int CountPoint(){
        return listPoint.Count;
    }

}
