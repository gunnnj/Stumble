using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailWing : MonoBehaviour
{
    [SerializeField] TMP_Text txtName;
    [SerializeField] Image imgSkin;
    [SerializeField] TMP_Text txtPrice;
    [SerializeField] TMP_Text txtSpeed;
    [SerializeField] Button btnBuy;
    [SerializeField] Button btnBuyAds;
    private ShopWings shopWings;

    void Start()
    {
        shopWings = GetComponentInParent<ShopWings>();
        btnBuy.onClick.AddListener(Buy);
    }


    public void UpdateDetail(string name, Sprite img, int price, int speed, bool isBuy){
        txtName.text = name;
        imgSkin.sprite = img;
        txtPrice.text = price.ToString();
        txtSpeed.text = "+"+speed.ToString();
        if(isBuy){
            btnBuy.gameObject.SetActive(false);
            btnBuyAds.gameObject.SetActive(false);
        }
        else{
            btnBuy.gameObject.SetActive(true);
            btnBuyAds.gameObject.SetActive(true);
        }
    }

    //Add button buy
    public void Buy(){
        EventShopWing.buySkin?.Invoke(shopWings.currentID);
        Debug.Log("Buy skin id:"+shopWings.currentID);
    }
}
