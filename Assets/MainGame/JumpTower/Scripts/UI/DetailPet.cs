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
    public int id;
    public bool canEquip = false;
    private BagPets bagPets;

    void Start()
    {
        bagPets = GetComponentInParent<BagPets>();
        btnEquip.onClick.AddListener(()=>EquipPet(id));
        btnUpgrade.onClick.AddListener(Upgrade);
    }

    public void UpdateDetail(int id){
        this.id = id;
        txtName.text = LoadDataPet.Instance.GetName(id);
        imgSkin.sprite = LoadDataPet.Instance.GetSprite(id);
        buffGold.text = "x"+LoadDataPet.Instance.GetBuffGold(id).ToString();
        txtType.text = LoadDataPet.Instance.GetTypeEgg(id).ToString();

        if(LoadDataPet.Instance.GetEquiped(id)){
            txtEquip.text = StateBtn.Unequip.ToString();
            canEquip = false;
        }
        else{
            txtEquip.text = StateBtn.Equip.ToString();
            canEquip = true;
        }

    }

    public void EquipPet(int id){
        if(canEquip){
            Debug.Log(id);
            UIClimb.Instance.buffGold += LoadDataPet.Instance.GetBuffGold(id);
            LoadDataPet.Instance.SetEquiped(id, true);
            UpdateDetail(id);
            //Call event equip
            EventShopPet.equipPet?.Invoke(id);
        }
        else{
            Debug.Log("Unequip "+id);
            UIClimb.Instance.buffGold -= LoadDataPet.Instance.GetBuffGold(id);
            LoadDataPet.Instance.SetEquiped(id, false);
            EventShopPet.unequipPet?.Invoke(id);
        }
        
    }
    public void Upgrade(){
        
    }
    
}
public enum StateBtn{
    Equip,
    Unequip
}
