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
    private GameObject currentPet;
    public int currentID;

    public static BagPets Instance;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        btnClose.onClick.AddListener(()=>CloseShop());
        btnDelete.onClick.AddListener(Delete);
        btnEquipBest.onClick.AddListener(EquipBest);

        SpawnItemPetInBag();
        detailPet.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public void SpawnItemPetInBag(){
        for(int i =0; i<LoadDataPet.Instance.GetAmountHas(); i++){
            GameObject item = Instantiate(ItemPetPrefab,Content);
            item.GetComponent<ItemPet>().LoadDataPetByID(LoadDataPet.Instance.GetIDPetInListHas(i));
        }
    }

    //Call when hatch egg
    public void SpawnAddItemPet(){
        GameObject item = Instantiate(ItemPetPrefab,Content);
        int count = LoadDataPet.Instance.GetAmountHas()-1;
        item.GetComponent<ItemPet>().LoadDataPetByID(LoadDataPet.Instance.GetIDPetInListHas(count));
    }

    private void PickPet(int id)
    {
        currentID = id;
        if(currentPet != null){
            Destroy(currentPet);
        }

        currentPet = Instantiate(LoadDataPet.Instance.GetPrefab(id),parent);
        UpdateDetail(id);
        EventShopPet.pickSkin?.Invoke(id);
    }

    public void UpdateDetail(int id){
        detailPet.UpdateDetail(id);
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
    public delegate void PickSkin(int id);
    public static PickSkin pickSkin;

    public delegate void BuySkin(int id);
    public static BuySkin buySkin;

    public delegate void EquipPet(int id);
    public static EquipPet equipPet;

    public delegate void UnequipPet(int id);
    public static UnequipPet unequipPet;

}
