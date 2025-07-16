using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailPet : MonoBehaviour
{
    [SerializeField] TMP_Text txtName;
    [SerializeField] Image imgSkin;
    [SerializeField] TMP_Text txtPrice;
    [SerializeField] TMP_Text buffGold;
    private ShopPets shopPets;

    void Start()
    {
        shopPets = GetComponentInParent<ShopPets>();
    }

    public void UpdateDetail(int id){
        txtName.text = LoadDataPet.Instance.GetName(id);
        imgSkin.sprite = LoadDataPet.Instance.GetSprite(id);
        txtPrice.text = LoadDataPet.Instance.GetPrice(id).ToString();
        buffGold.text = "x"+LoadDataPet.Instance.GetBuffGold(id).ToString();
    }
    //Add button buy
    public void Buy(){
        Debug.Log("Buy pet id: "+ shopPets.currentID);
    }
}
