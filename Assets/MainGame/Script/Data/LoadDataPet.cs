using UnityEngine;

public class LoadDataPet : MonoBehaviour
{
    [SerializeField] SkinPets skinPets;

    public static LoadDataPet Instance;

    void Awake()
    {
        Instance = this;
    }

    public GameObject GetPrefab(int id){
        return skinPets.pets[id].prefab;
    }
    public Sprite GetSprite(int id){
        return skinPets.pets[id].image;
    }
    public int GetPrice(int id){
        return skinPets.pets[id].price;
    }
    public bool GetPurchased(int id){
        return skinPets.pets[id].isPurchased;
    }
    public float GetBuffGold(int id){
        return skinPets.pets[id].buffGold;
    }
    public string GetName(int id){
        return skinPets.pets[id].name;
    }
    public void SetPurchased(int id){
        skinPets.pets[id].isPurchased = true;
    }
}
