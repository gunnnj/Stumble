using System.Collections.Generic;
using DiasGames.Controller;
using UnityEngine;
using UnityEngine.UI;

public class ShopWings : MonoBehaviour
{
    [SerializeField] List<ItemWing> itemWings;
    [SerializeField] CSPlayerController player;
    [SerializeField] Button btnClose;
    [SerializeField] DetailWing detailWing;
    [SerializeField] UIClimb uIClimb;
    public Transform transParentWing;
    public GameObject currentWings;
    public int currentID;
    public bool isResetIdLast = false;
    
    public static ShopWings Instance;

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        EventShopWing.buySkin += BuySkinWing;
    }
    void OnDisable()
    {
        EventShopWing.buySkin -= BuySkinWing;
    }
    
    void Start()
    {
        transParentWing = player.transform.GetChild(1).GetChild(1).GetChild(2);

        btnClose.onClick.AddListener(()=>ActiveShop(false));

        SetIDItem();
        
        if(isResetIdLast){
            PlayerPrefs.SetInt(ConstString.LastIDWing,0);
            PlayerPrefs.SetInt(ConstString.CurrentIDWing,-1);
            PlayerPrefs.Save();
        }

        PickWing(0);
        currentID = PlayerPrefs.GetInt(ConstString.CurrentIDWing,-1);

        if(currentID>=0){
            currentWings = Instantiate(LoadDataWing.Instance.GetPrefab(currentID),transParentWing);
            EventShopWing.pickSkin?.Invoke(currentID);
        }
        // else{
        //     Destroy(currentWings);
        // }
        

        CheckLockWing();

        gameObject.SetActive(false);
    }

    public void SetIDItem(){
        for(int i =0; i<itemWings.Count; i++){
            int id = i;
            itemWings[i].ID = id;
        }
    }

    public void CheckLockWing(){
        int lastID = PlayerPrefs.GetInt(ConstString.LastIDWing,0);
        for(int i=0; i<=lastID; i++){
            itemWings[i].Lock(false);
            itemWings[i].EnableButton();
        }
    }

    private void BuySkinWing(int id)
    {
        //Check enough gold in UIClimb
        int price = LoadDataWing.Instance.GetPrice(id);
        if(uIClimb.totalGold<price) {
            Debug.Log("K đủ tiền!");
            return;
        }
        if(LoadDataWing.Instance.GetPurchased(id)){
            Debug.Log("Skin đã được mua!");
        }
        else{
            uIClimb.BuySkin(id);

            if(currentWings!=null){
                Destroy(currentWings);
            }
            
            currentWings = Instantiate(LoadDataWing.Instance.GetPrefab(id),transParentWing);

            if(id<itemWings.Count-1){
                itemWings[id+1].Lock(false);
                itemWings[id+1].EnableButton();
            }

            PlayerPrefs.SetInt(ConstString.CurrentIDWing,id);
            PlayerPrefs.SetInt(ConstString.LastIDWing,id);

            PlayerPrefs.Save();

            UpdateDetail(id);
            EventShopWing.pickSkin?.Invoke(id);
        }

        
    }

    private void ActiveShop(bool value)
    {
        gameObject.SetActive(value);
    }

    public void PickWing(int id){
        currentID = id;

        if(LoadDataWing.Instance.GetPurchased(id)){
            if(currentWings!=null){
                Destroy(currentWings);
            }
            
            currentWings = Instantiate(LoadDataWing.Instance.GetPrefab(id),transParentWing);
            PlayerPrefs.SetInt(ConstString.CurrentIDWing,id);
            PlayerPrefs.Save();
        }

        UpdateDetail(id);
        EventShopWing.pickSkin?.Invoke(id);

    }

    public void UpdateDetail(int id){
        string name = LoadDataWing.Instance.GetName(id);
        Sprite img = LoadDataWing.Instance.GetSprite(id);
        int price = LoadDataWing.Instance.GetPrice(id);
        int speed = LoadDataWing.Instance.GetBuffSpeed(id);
        bool isBuy = LoadDataWing.Instance.GetPurchased(id);

        detailWing.UpdateDetail(name,img,price,speed, isBuy);
    }

}

public class EventShopWing{
    public delegate void PickSkin(int id);
    public static PickSkin pickSkin;

    public delegate void BuySkin(int id);
    public static BuySkin buySkin;

}
