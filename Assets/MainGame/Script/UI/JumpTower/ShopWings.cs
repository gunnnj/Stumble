using System;
using System.Collections.Generic;
using DiasGames.Controller;
using UnityEngine;
using UnityEngine.UI;

public class ShopWings : MonoBehaviour
{
    [SerializeField] ListWing listWing;
    [SerializeField] List<Button> btnWings;
    [SerializeField] CSPlayerController player;
    [SerializeField] Button btnClose;
    [SerializeField] DetailWing detailWing;
    public Transform transParentWing;
    public GameObject currentWings;

    public int currentID;
    
    
    void Start()
    {
        transParentWing = player.transform.GetChild(1).GetChild(1).GetChild(2);

        btnClose.onClick.AddListener(()=>ActiveShop(false));

        for(int i =0; i<btnWings.Count; i++){
            int id = i;
            btnWings[i].onClick.AddListener(()=>PickWing(id));
        }
    }

    private void ActiveShop(bool value)
    {
        gameObject.SetActive(value);
    }

    public void PickWing(int id){
        currentID = id;

        if(currentWings!=null){
            Destroy(currentWings);
        }
        
        currentWings = Instantiate(listWing.skins[id].prefab,transParentWing);
        UpdateDetail(id);
        EventShopWing.pickSkin?.Invoke(id);

    }

    public void UpdateDetail(int id){
        string name = listWing.skins[id].name;
        Sprite img = listWing.skins[id].image;
        int price = listWing.skins[id].price;
        int speed = listWing.skins[id].buffSpeed;

        detailWing.UpdateDetail(name,img,price,speed);
    }

}

public class EventShopWing{
    public delegate void PickSkin(int id);
    public static PickSkin pickSkin;

    public delegate void BuySkin(int id);
    public static BuySkin buySkin;

}
