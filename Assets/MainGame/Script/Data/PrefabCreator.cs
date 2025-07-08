using System.Linq;
using UnityEngine;
[ExecuteInEditMode]
public class PrefabCreator : MonoBehaviour
{
    public GameObject[] listGameObject;
    public int index = 2;
    public GameObject go;

    public void PrevPrefab(){
        index --;
        if(index<0){
            index = listGameObject.Count()-1;
        }
        
        CreatePrefab();
    }
    public void NextPrefab(){
        index ++;
        if(index==listGameObject.Count()){
            index = 0;
        }
        
        CreatePrefab();
    }
    public void CreatePrefab(){
        if(go!=null){
            DestroyImmediate(go); 
        }
        go = Instantiate(listGameObject[index],transform.position,Quaternion.identity);
    }
    public void Save(){
        go = null;
    }
    public void RotationY(){
        go.transform.eulerAngles += new Vector3(0,90,0);
    }
    public void RotationX(){
        go.transform.eulerAngles += new Vector3(90,0,0);
    }
    public void RotationZ(){
        go.transform.eulerAngles += new Vector3(0,0,90);
    }
}




