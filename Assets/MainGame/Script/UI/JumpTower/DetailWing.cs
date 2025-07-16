using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailWing : MonoBehaviour
{
    [SerializeField] TMP_Text txtName;
    [SerializeField] Image imgSkin;
    [SerializeField] TMP_Text txtPrice;
    [SerializeField] TMP_Text txtSpeed;
    private ShopWings shopWings;

    void Start()
    {
        shopWings = GetComponentInParent<ShopWings>();
    }


    public void UpdateDetail(string name, Sprite img, int price, int speed){
        txtName.text = name;
        imgSkin.sprite = img;
        txtPrice.text = price.ToString();
        txtSpeed.text = "+"+speed.ToString();
    }

    //Add button buy
    public void Buy(){
        EventShopWing.buySkin?.Invoke(shopWings.currentID);
        Debug.Log("Buy skin id:"+shopWings.currentID);
    }
}
