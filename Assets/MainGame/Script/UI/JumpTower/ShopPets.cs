using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPets : MonoBehaviour
{
    [SerializeField] List<Button> btnPets;
    [SerializeField] Transform parent;
    [SerializeField] Button btnClose;
    [SerializeField] DetailPet detailPet;
    public GameObject currentPet;
    public int currentID;

    void Start()
    {
        btnClose.onClick.AddListener(()=>CloseShop());

        for(int i =0; i<btnPets.Count; i++){
            int id = i;
            btnPets[i].onClick.AddListener(()=>PickPet(id));
        }
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
}
public class EventShopPet{
    public delegate void PickSkin(int id);
    public static PickSkin pickSkin;

    public delegate void BuySkin(int id);
    public static BuySkin buySkin;

}
