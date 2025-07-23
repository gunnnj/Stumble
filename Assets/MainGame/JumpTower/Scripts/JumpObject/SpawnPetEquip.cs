using UnityEngine;

public class SpawnPetEquip : MonoBehaviour
{

    void OnEnable()
    {
        EventShopPet.equipPet += SpawnPet;
    }
    void OnDisable()
    {
        EventShopPet.equipPet -= SpawnPet;
    }

    public void SpawnPet(int id){
        Transform trans = SpawnPosPet.GetEmptyPos();
        if(trans!=null){
            GameObject go = Instantiate(LoadDataPet.Instance.GetPrefab(id),trans);
        }
        else{
            Debug.Log("No enough slot");
        }
        
    }

}
