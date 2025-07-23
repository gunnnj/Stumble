using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ListWing", menuName = "DataSkin/ListWing")]
public class ListWing : ScriptableObject
{
    public List<Skin> skins;
}

[System.Serializable]
public class Skin
{
    public GameObject prefab;
    public Sprite image;      
    public int price;         
    public bool isPurchased;  
    public int buffSpeed;
    public string name;
}
