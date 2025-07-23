using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPet : MonoBehaviour
{
    public int ID;
    [SerializeField] TMP_Text txtBuffGold;
    [SerializeField] Image imgPet;
    [SerializeField] GameObject tickEquip;
    private Button btnItemPet;

    void Start()
    {
        btnItemPet = GetComponent<Button>();
        btnItemPet.onClick.AddListener(UpdateDetail);
    }

    private void UpdateDetail()
    {
        BagPets.Instance.detailPet.gameObject.SetActive(true);
        BagPets.Instance.detailPet.UpdateDetail(ID);
    }

    public void LoadDataPetByID(int id){
        ID = id;
        SetTxtGold(LoadDataPet.Instance.GetBuffGold(id));
        SetImagePet(LoadDataPet.Instance.GetSprite(id));
        SetEquip(LoadDataPet.Instance.GetEquiped(id));
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
}
