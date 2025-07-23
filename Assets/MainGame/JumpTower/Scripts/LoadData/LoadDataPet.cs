using System.Collections.Generic;
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
    //Lấy bool có sở hữu pet 
    public bool GetHasPet(int id){
        return skinPets.pets[id].hasPet;
    }
    public void SetHasPet(int id){
        skinPets.pets[id].hasPet = true;
    }
    //Pet đã equip chưa?
    public bool GetEquiped(int id){
        return skinPets.pets[id].equiped;
    }
    public bool SetEquiped(int id, bool value){
        return skinPets.pets[id].equiped = value;
    }
    public float GetBuffGold(int id){
        return skinPets.pets[id].buffGold;
    }
    public string GetName(int id){
        return skinPets.pets[id].name;
    }
    //Số lượng pet tổng
    public int GetAmountPet(){
        return skinPets.pets.Count;
    }
    //Số lượng pet có thể trang bị
    public int GetSlotPet(){
        return skinPets.listEquip.Count;
    }
    //Số lượng có trong bag
    public int GetAmountHas(){
        return skinPets.listHas.Count;
    }
    public void AddSlotPet(int idPet){
        skinPets.AddListEquip(idPet);
    }
    public void AddHasPet(int idPet){
        skinPets.AddListHas(idPet);
    }
    //Lấy chỉ số skin equiped
    public int GetIDPetEquip(int value){
        return skinPets.listEquip[value];
    }
    public List<int> GetListEquip(){
        return skinPets.listEquip;
    }
    //Lấy chỉ số list pet
    public int GetIDPetInListHas(int value){
        return skinPets.listHas[value];
    }
    public List<int> GetListHas(){
        return skinPets.listHas;
    }
    public TypeEgg GetTypeEgg(int id){
        return skinPets.pets[id].typeEgg;
    }
    
}
