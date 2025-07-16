using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIClimb : MonoBehaviour
{
    [SerializeField] GameObject ShopWing;
    [SerializeField] GameObject ShopPet;
    [SerializeField] Button btnOpenShopWing;
    [SerializeField] Button btnOpenShopPet;
    [SerializeField] float minPosY;
    [SerializeField] float maxPosY;
    [SerializeField] Transform player;
    [SerializeField] Slider slider;
    [SerializeField] Slider sliderIcon;
    [SerializeField] TMP_Text meter;
    [SerializeField] GameObject goldGet;
    [SerializeField] TMP_Text txtGoldGet;
    [SerializeField] TMP_Text txtTotalGold;
    [Header("1 meter = ratio (coin)")]
    [SerializeField] float ratio = 100f;
    public bool isResetGold = false;

    public float oldG;
    public float totalGold;
    public float g;

    void OnEnable()
    {
        EventShopWing.buySkin += BuySkin;
    }

    void OnDisable()
    {
        EventShopWing.buySkin -= BuySkin;
    }

    void Start()
    {
        // arrow.rectTransform.DOAnchorPosY(maxPositionY, 2f);
        // minRectY = arrow.rectTransform.position.y;
        // rectX = arrow.rectTransform.position.x;
        btnOpenShopWing.onClick.AddListener(OpenShopWing);
        btnOpenShopPet.onClick.AddListener(OpenShopPet);

        oldG = 0;
        SetActiveGoldText(false);

        if(isResetGold){
            PlayerPrefs.SetInt(ConstString.TotalGold,0);
        }

        totalGold = PlayerPrefs.GetInt(ConstString.TotalGold,0);
        float goldtext = totalGold/ 1000;
        txtTotalGold.text = goldtext.ToString("F2")+"K";

        
    }

    void Update()
    {
        float playerY = player.position.y;

        // rectY = minRectY - (playerY/(maxPosY-minPosY))*(maxRectY-minRectY); 
        if(playerY>=minPosY){
            slider.value = playerY/(maxPosY-minPosY);
            sliderIcon.value = playerY/(maxPosY-minPosY);
        }
        else{
            slider.value = 0;
            sliderIcon.value = 0;
            txtGoldGet.text = "0";
            if(oldG>1){
                totalGold += oldG*ratio;

                PlayerPrefs.SetInt(ConstString.TotalGold,(int)totalGold);

                float goldtext = totalGold / 1000;
                txtTotalGold.text = goldtext.ToString("F2")+"K";
            }
            
            oldG = 0;
        }
        
        // // Cập nhật vị trí của arrow (chỉ thay đổi Y, giữ nguyên X và Z)
        // arrow.rectTransform.position = new Vector3(rectX, rectY, 0);

        UpdateGoldText(playerY);
    }

    public void UpdateGoldText(float positionY){
        if(positionY>=minPosY){
            g = (int)(positionY);
            meter.text = g+" m";
            if (g > oldG)
            {
                // Tính số vàng tương ứng
                float gold = g * ratio;

                // Hiển thị số vàng
                if (gold >= 1000)
                {
                    gold /= 1000;
                    txtGoldGet.text = gold.ToString("F2") + "K"; 
                }
                else
                {
                    txtGoldGet.text = gold.ToString("F0"); 
                }


                oldG = g;
            }
            
        }
    }
    public void SetActiveGoldText(bool value){
        goldGet.SetActive(value);
    }
    private void OpenShopWing()
    {
        ShopWing.SetActive(true);
    }
    private void OpenShopPet(){
        ShopPet.SetActive(true);
    }
    private void BuySkin(int id)
    {
        int price = LoadDataWing.Instance.GetPrice(id);
        if(totalGold<price) {
            Debug.Log("K đủ tiền!");
            return;
        }
        if(LoadDataWing.Instance.GetPurchased(id)){
            Debug.Log("Skin đã được mua!");
            return;
        }
        totalGold -= price;

        PlayerPrefs.SetInt(ConstString.TotalGold,(int)totalGold);

        float goldtext = totalGold / 1000;
        txtTotalGold.text = goldtext.ToString("F2")+"K";

        LoadDataWing.Instance.SetPurchased(id);
    }
}
