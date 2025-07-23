using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ListWing", menuName = "DataSkin/ListPet")]
public class SkinPets : ScriptableObject
{
    public List<int> listEquip;
    public List<int> listHas;
    public List<Pet> pets;

    public void AddListHas(int value){
        listHas.Add(value);
    }
    public void AddListEquip(int value){
        listEquip.Add(value);
    }

}

[System.Serializable]
public class Pet
{
    public GameObject prefab;
    public Sprite image;   
    public bool equiped;         
    public bool hasPet;  
    public float buffGold;
    public string name;
    public TypeEgg typeEgg;
}

public enum TypeEgg{
    Rare,
    Epic,
    Legend,
    Mysterious,
    Immortal
}
