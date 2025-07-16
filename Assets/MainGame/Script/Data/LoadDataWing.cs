using UnityEngine;

public class LoadDataWing : MonoBehaviour
{
    [SerializeField] ListWing listWing;

    public static LoadDataWing Instance;

    void Awake()
    {
        Instance = this;
    }

    public GameObject GetPrefab(int id){
        return listWing.skins[id].prefab;
    }
    public Sprite GetSprite(int id){
        return listWing.skins[id].image;
    }
    public int GetPrice(int id){
        return listWing.skins[id].price;
    }
    public bool GetPurchased(int id){
        return listWing.skins[id].isPurchased;
    }
    public int GetBuffSpeed(int id){
        return listWing.skins[id].buffSpeed;
    }
    public string GetName(int id){
        return listWing.skins[id].name;
    }
    public void SetPurchased(int id){
        listWing.skins[id].isPurchased = true;
    }
}
