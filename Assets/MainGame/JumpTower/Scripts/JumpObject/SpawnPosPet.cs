using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class SpawnPosPet : MonoBehaviour
{
    public GameObject position;
    public int amountPos = 0;
    private static List<GameObject> list = new List<GameObject>();

    void Start()
    {
        SpawnPosition();
    }
    [ContextMenu("Spawn")]
    public void SpawnPosition()
    {
        amountPos++;
        Debug.Log("So luong pet: " + amountPos);
        int col = Mathf.CeilToInt(amountPos / 5f);
        
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject pos = Instantiate(position, this.transform);
                pos.transform.localPosition = new Vector3(0.7f * j, 0, -0.7f * i);
                list.Add(pos);
            }
        }
    }
    public static GameObject GetPosition(int value){
        return list[value];
    }
    public static Transform GetEmptyPos(){
        int amount=0;
        int amountPos = LoadDataPet.Instance.GetSlotPet();
        foreach(var item in list){
            if(item.transform.childCount>0){
                amount++;
            }
            else{
                if(amount<=amountPos-1){
                    return item.transform;
                }
            }
        }
        return null;
    }
}