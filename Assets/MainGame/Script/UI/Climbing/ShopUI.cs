using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Button btnSkin;
    public Button btnCloseShop;
    public GameObject panelShop;

    void Start()
    {
        btnSkin.onClick.AddListener(()=>DisplayShop(true));
        btnCloseShop.onClick.AddListener(()=>DisplayShop(false));
        DisplayShop(false);
    }

    private void DisplayShop(bool value)
    {
        panelShop.SetActive(value);
    }
}
