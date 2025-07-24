using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPetEquip : MonoBehaviour
{
    List<dicID_Object> listID_Obj = new List<dicID_Object>();

    void OnEnable()
    {
        EventShopPet.equipPet += SpawnPet;
        EventShopPet.unequipPet += DestroyPet;
    }

    void OnDisable()
    {
        EventShopPet.equipPet -= SpawnPet;
        EventShopPet.unequipPet -= DestroyPet;
    }

    void Start()
    {
        SpawnListEquipPet();
    }

    private void SpawnListEquipPet()
    {
        List<int> listE = LoadDataPet.Instance.GetListEquip();
        for(int i =0; i<listE.Count; i++){
            if(listE[i]<0) continue;
            SpawnPet(i,LoadDataPet.Instance.GetValueByIDListHas(listE[i]));
        }
    }

    public void SpawnPet(int idEquip, int idSkin){
        Transform trans = SpawnPosPet.GetEmptyPos();
        GameObject goPet;
        if(trans!=null){

            //Thêm vào list
            goPet = Instantiate(LoadDataPet.Instance.GetPrefab(idSkin),trans);

            dicID_Object temp = new dicID_Object();
            temp.Id = idEquip;
            temp.go = goPet;

            listID_Obj.Add(temp);

        }
        else{
            Debug.Log("No enough slot");
        }
        
    }
    private void DestroyPet(int idEquip, int idSkin)
    {
        dicID_Object temp = new dicID_Object();

        foreach(var item in listID_Obj){
            if(item.Id == idEquip){
                temp = item;
            }
        }
        Destroy(temp.go);
        listID_Obj.Remove(temp);
    }

}
public class dicID_Object{
    public int Id {get; set;}
    public GameObject go {get; set;}
}
