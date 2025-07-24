using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPet : MonoBehaviour
{
    public int IDinList;
    public int IDinSkin;
    [SerializeField] TMP_Text txtBuffGold;
    [SerializeField] Image imgPet;
    [SerializeField] GameObject tickEquip;
    private Button btnItemPet;

    void OnEnable()
    {
        EventShopPet.equipPet += UpdateTick;
        EventShopPet.unequipPet += UpdateTick;
    }

    void OnDisable()
    {
        EventShopPet.equipPet -= UpdateTick;
        EventShopPet.unequipPet += UpdateTick;
    }

    void Start()
    {
        btnItemPet = GetComponent<Button>();
        btnItemPet.onClick.AddListener(UpdateDetail);
        // tickEquip.SetActive(false);
        
        if(BagPets.Instance.detailPet.CheckValueInListEquip(IDinList)){
            SetEquip(false);
        }
        else{
            SetEquip(true);
        }
    }

    private void UpdateDetail()
    {
        BagPets.Instance.detailPet.gameObject.SetActive(true);
        BagPets.Instance.detailPet.UpdateDetail(IDinSkin, IDinList);
        EventShopPet.pickPet?.Invoke(IDinList);
    }

    public void LoadDataPetByIDSkin(int id, int idList){
        IDinSkin = id;
        SetTxtGold(LoadDataPet.Instance.GetBuffGold(id));
        SetImagePet(LoadDataPet.Instance.GetSprite(id));
        // SetEquip(LoadDataPet.Instance.GetEquiped(id));
        IDinList = idList;
    }

    public void SetTxtGold(float value){
        txtBuffGold.text =  "x"+value.ToString();
    }
    public void SetImagePet(Sprite img){
        imgPet.sprite = img;
    }
    public void SetEquip(bool value){
        tickEquip.SetActive(value);
    }

    private void UpdateTick(int idEquip, int idSkin)
    {
        if(BagPets.Instance.detailPet.CheckValueInListEquip(IDinList)){
            SetEquip(false);
        }
        else{
            SetEquip(true);
        }
    }

}
