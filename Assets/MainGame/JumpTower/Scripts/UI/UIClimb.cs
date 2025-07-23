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
    [SerializeField] public float buffGold = 1.5f;
    public bool isResetGold = false;

    public float oldG;
    public float totalGold;
    public float g;
    public static UIClimb Instance;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        // EventShopWing.buySkin += BuySkin;
    }

    void OnDisable()
    {
        // EventShopWing.buySkin -= BuySkin;
    }

    void Start()
    {
        btnOpenShopWing.onClick.AddListener(OpenShopWing);
        btnOpenShopPet.onClick.AddListener(OpenShopPet);

        oldG = 0;
        SetActiveGoldText(false);

        if(isResetGold){
            PlayerPrefs.SetInt(ConstString.TotalGold,0);
        }

        totalGold = PlayerPrefs.GetInt(ConstString.TotalGold,0);

        // if(totalGold>1000){
        //     float goldtext = totalGold / 1000;
        //     txtTotalGold.text = goldtext.ToString("F2")+"K";
        // }
        // else{
        //     txtTotalGold.text = totalGold.ToString("F0");
        // }

        txtTotalGold.text = Formatter.FormatMoney(totalGold);


        
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
                totalGold += oldG*ratio*buffGold;
                Debug.Log("Add "+ oldG*ratio*buffGold + " gold");

                PlayerPrefs.SetInt(ConstString.TotalGold,(int)totalGold);

                PlayerPrefs.Save();

                txtTotalGold.text = Formatter.FormatMoney(totalGold);
                // if(totalGold>1000){
                //     float goldtext = totalGold / 1000;
                //     txtTotalGold.text = goldtext.ToString("F2")+"K";
                // }
                // else{
                //     txtTotalGold.text = totalGold.ToString("F0");
                // }
                
            }
            
            oldG = 0;
        }

        UpdateGoldText(playerY);
    }

    public void UpdateGoldText(float positionY){
        if(positionY>=minPosY){
            g = (int)(positionY);
            meter.text = g+" m";
            if (g > oldG)
            {
                // Tính số vàng tương ứng
                float gold = g * ratio * buffGold;

                // Hiển thị số vàng
                // if (gold >= 1000)
                // {
                //     gold /= 1000;
                //     txtGoldGet.text = gold.ToString("F2") + "K"; 
                // }
                // else
                // {
                //     txtGoldGet.text = gold.ToString("F0"); 
                // }

                txtGoldGet.text = Formatter.FormatMoney(gold);

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
    public void BuySkin(int id)
    {
        int price = LoadDataWing.Instance.GetPrice(id);
        totalGold -= price;

        PlayerPrefs.SetInt(ConstString.TotalGold,(int)totalGold);
        PlayerPrefs.Save();

        txtTotalGold.text = Formatter.FormatMoney(totalGold);

        LoadDataWing.Instance.SetPurchased(id);
    }
    public void MinusTotalGold(int value){
        totalGold -= value;
        PlayerPrefs.SetInt(ConstString.TotalGold,(int)totalGold);
        PlayerPrefs.Save();
        txtTotalGold.text = Formatter.FormatMoney(totalGold);

    }
}
