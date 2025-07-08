using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dropable : MonoBehaviour
{
    [SerializeField] float timeReset = 2f;
    [SerializeField] float force = 500f;
    public ObjectDrop[] objDrops;

    void Start()
    {
        foreach(var item in objDrops){
            item.gameObject.SetActive(false);
        }

        StartCoroutine(LoopDrop());
    }

    public void Drop(int id){
        if(!objDrops[id].gameObject.activeSelf){
            objDrops[id].ResetPosition();
            objDrops[id].AddForce(force,-transform.forward);
            objDrops[id].gameObject.SetActive(true);
        }
    }

    public IEnumerator LoopDrop(){
        int index = 0;
        while(true){
            yield return new WaitForSeconds(timeReset);
            Drop(index);
            index++;
            if(index==objDrops.Count()-1){
                index = 0;
            }
        }
        
        
    }
}
