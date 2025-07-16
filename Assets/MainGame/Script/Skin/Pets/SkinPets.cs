using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ListWing", menuName = "DataSkin/ListPet")]
public class SkinPets : ScriptableObject
{
    public List<Pet> pets;
}

[System.Serializable]
public class Pet
{
    public GameObject prefab;
    public Sprite image;      
    public int price;         
    public bool isPurchased;  
    public float buffGold;
    public string name;
}
