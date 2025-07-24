using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopEggs : MonoBehaviour
{
    [SerializeField] int Price;
    [SerializeField] Button btnClose;
    [SerializeField] Button btnHatch;
    [SerializeField] Button btnHatchEgg;
    [SerializeField] GameObject HatchEgg;
    [SerializeField] Image imgPetHatch;
    [SerializeField] List<int> idPetEgg;
    [SerializeField] TMP_Text txtType;

    void Start()
    {
        btnClose.onClick.AddListener(Close);
        btnHatch.onClick.AddListener(()=>Hatch());
        btnHatchEgg.onClick.AddListener(()=>HatchEgg.SetActive(false));
        gameObject.SetActive(false);
    }

    private void Hatch()
    {
        //Check enough gold
        if(UIClimb.Instance.totalGold>=Price){
            UIClimb.Instance.MinusTotalGold(Price);
        }
        else{
            Debug.Log("No enough gold buy egg");
            return;
        }
        
        //Add animation hatch egg

        //RandomEgg
        int type = RandomEgg.RandomWithRaito(45,28,16,8);
        Debug.Log("Egg type: "+type);
        imgPetHatch.sprite =  LoadDataPet.Instance.GetSprite(idPetEgg[type]);
        txtType.text = LoadDataPet.Instance.GetTypeEgg(idPetEgg[type]).ToString();
        LoadDataPet.Instance.AddHasPet(idPetEgg[type]);
        BagPets.Instance.SpawnAddItemPet();
        HatchEgg.SetActive(true);

    }

    private void Close()
    {
        gameObject.SetActive(false);
    }
}
