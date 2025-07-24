using System;
using System.Collections.Generic;
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
    public float buffGold = 1f;
    public float sumBuff = 0;
    public bool isResetGold = false;

    public float oldG;
    public float totalGold;
    public float g;
    public static UIClimb Instance;

    void Awake()
    {
        Instance = this;
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


        txtTotalGold.text = Formatter.FormatMoney(totalGold);

        UpdateBuffGold();
        
    }

    void Update()
    {
        float playerY = player.position.y;

        if(playerY>=minPosY){
            slider.value = playerY/(maxPosY-minPosY);
            sliderIcon.value = playerY/(maxPosY-minPosY);
        }
        else{
            slider.value = 0;
            sliderIcon.value = 0;
            txtGoldGet.text = "0";
            if(oldG>1){

                if(sumBuff>0){
                    totalGold += oldG*ratio*sumBuff;
                }
                else{
                    totalGold += oldG*ratio*buffGold;
                }

                totalGold += oldG*ratio*buffGold;
                Debug.Log("Add "+ oldG*ratio*buffGold + " gold");

                PlayerPrefs.SetInt(ConstString.TotalGold,(int)totalGold);

                PlayerPrefs.Save();

                txtTotalGold.text = Formatter.FormatMoney(totalGold);
                
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
                float gold;
                // Tính số vàng tương ứng
                if(sumBuff>0){
                    gold = g * ratio * sumBuff;
                }
                else{
                    gold = g * ratio * buffGold;
                }

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
    public void UpdateBuffGold(){
        sumBuff = 0;
        List<int> listE = LoadDataPet.Instance.GetListEquip();
        foreach(var item in listE){
            if(item>=0){
                
                int idSkin = LoadDataPet.Instance.GetValueByIDListHas(item);
                Debug.Log(idSkin);
                sumBuff += LoadDataPet.Instance.GetBuffGold(idSkin);
                Debug.Log(sumBuff);
            }    
        }
    }
}
