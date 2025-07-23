using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemWing : MonoBehaviour
{
    [SerializeField] public int ID;
    [SerializeField] GameObject iconLock;
    private Button btnItem;

    void Start()
    {
        btnItem = GetComponent<Button>();
        btnItem.onClick.AddListener(PickItem);
        btnItem.interactable = false;
    }

    private void PickItem()
    {
        ShopWings.Instance.PickWing(ID);
    }

    public void Lock(bool value){
        iconLock.SetActive(value);
    }
    public void EnableButton(){
        btnItem.interactable = true;
    }
}
