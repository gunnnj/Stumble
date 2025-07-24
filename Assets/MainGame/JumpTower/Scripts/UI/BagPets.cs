using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPets : MonoBehaviour
{
    [SerializeField] Transform Content;
    [SerializeField] GameObject ItemPetPrefab;
    [SerializeField] Transform parent;
    [SerializeField] Button btnEquipBest;
    [SerializeField] Button btnDelete;
    [SerializeField] Button btnClose;
    [SerializeField] public DetailPet detailPet;
    [SerializeField] Button btnAddSlot;

    private int amountSlotPet;
    private int amountPetHas;
    private GameObject currentPet;
    public int currentID;

    public static BagPets Instance;

    void Awake()
    {
        Instance = this;
        SpawnItemPetInBag();
    }
    void OnEnable()
    {
        EventShopPet.pickPet += PickPet;
    }
    void OnDisable()
    {
        EventShopPet.pickPet -= PickPet;
    }

    void Start()
    {
        btnClose.onClick.AddListener(()=>CloseShop());
        btnDelete.onClick.AddListener(Delete);
        btnEquipBest.onClick.AddListener(EquipBest);

        detailPet.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    //Tạo các item pet đã có trong bag
    public void SpawnItemPetInBag(){
        for(int i =0; i<LoadDataPet.Instance.GetAmountHas(); i++){
            GameObject item = Instantiate(ItemPetPrefab,Content);
            item.GetComponent<ItemPet>().LoadDataPetByIDSkin(LoadDataPet.Instance.GetValueByIDListHas(i),i);
        }
    }

    //Call when hatch egg
    public void SpawnAddItemPet(){
        GameObject item = Instantiate(ItemPetPrefab,Content);
        int count = LoadDataPet.Instance.GetAmountHas()-1;
        item.GetComponent<ItemPet>().LoadDataPetByIDSkin(LoadDataPet.Instance.GetValueByIDListHas(count),count);
    }

    private void PickPet(int id)
    {
        currentID = id;

    }

    private void CloseShop()
    {
        gameObject.SetActive(false);
    }

    //Add button Equip best
    public void EquipBest(){

    }
    //Add button Delete
    public void Delete(){

    }
}
public class EventShopPet{
    public delegate void PickPet(int id);
    public static PickPet pickPet;

    public delegate void EquipPet(int idEquip,int idSkin);
    public static EquipPet equipPet;

    //Lấy id của List E
    public delegate void UnequipPet(int idEquit,int idSkin);
    public static UnequipPet unequipPet;

}
