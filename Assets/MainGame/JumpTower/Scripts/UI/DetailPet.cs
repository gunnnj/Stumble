using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailPet : MonoBehaviour
{
    [SerializeField] TMP_Text txtName;
    [SerializeField] Image imgSkin;
    [SerializeField] TMP_Text txtEquip;
    [SerializeField] TMP_Text txtType;
    [SerializeField] TMP_Text buffGold;
    [SerializeField] Button btnEquip;
    [SerializeField] Button btnUpgrade;
    public int idSkin;
    public int idList;
    public int idEquip;
    public bool canEquip = false;
    private BagPets bagPets;

    void Start()
    {
        bagPets = GetComponentInParent<BagPets>();
        btnEquip.onClick.AddListener(()=>EquipPet(idSkin));
        btnUpgrade.onClick.AddListener(Upgrade);
    }

    public void UpdateDetail(int id, int idList){
        this.idSkin = id;
        this.idList = idList;
        txtName.text = LoadDataPet.Instance.GetName(id);
        imgSkin.sprite = LoadDataPet.Instance.GetSprite(id);
        buffGold.text = "x"+LoadDataPet.Instance.GetBuffGold(id).ToString();
        txtType.text = LoadDataPet.Instance.GetTypeEgg(id).ToString();

        //Trạng thái nút Equip
        if(CheckValueInListEquip(idList)){
            txtEquip.text = "Equip";
            canEquip = true;
            List<int> listEquip = LoadDataPet.Instance.GetListEquip();
            for(int i=0; i<listEquip.Count; i++){
                if(listEquip[i]<0){
                    canEquip = true;
                    idEquip = i;
                    break;
                }
            }
        }
        else{
            txtEquip.text = "Unequip";
            canEquip = false;
            idEquip = LoadDataPet.Instance.GetIDByValueListEquip(idList);
        }


        
       
    }

    public void EquipPet(int id){
        if(canEquip){
            // UIClimb.Instance.buffGold += LoadDataPet.Instance.GetBuffGold(id);            

            if(CheckValueInListEquip(idList)){
                LoadDataPet.Instance.SetValueByIDListEquip(idEquip,idList);
                //Call event equip
                EventShopPet.equipPet?.Invoke(idEquip, id);
            }
            
            UpdateDetail(id,idList);
            
        }
        else{
            Debug.Log("Unequip idE "+idEquip);
            
            // UIClimb.Instance.buffGold -= LoadDataPet.Instance.GetBuffGold(id);

            LoadDataPet.Instance.SetValueByIDListEquip(idEquip,-1);

            EventShopPet.unequipPet?.Invoke(idEquip, idList);

            UpdateDetail(id,idList);
        }
        
    }
    public void Upgrade(){
        
    }
    public bool CheckValueInListEquip(int IdinList){
        List<int> list = LoadDataPet.Instance.GetListEquip();
        foreach(var item in list){
            if(item == IdinList){
                return false;
            }
        }
        return true;
    }
    
}
public enum StateBtn{
    Equip,
    Unequip
}
