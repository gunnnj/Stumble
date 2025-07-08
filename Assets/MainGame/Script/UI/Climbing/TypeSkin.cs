using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypeSkin : MonoBehaviour
{
    public List<BtnPickType> listBtnType = new List<BtnPickType>();
    public List<Button> listBtn = new List<Button>();
    public int currentIndex=0;
    public TMP_Text txtNameType;

    void Start()
    {
        GetListType();
        listBtnType[currentIndex].SetOutline(true);
        listBtnType[currentIndex].DisplayShopTypeSkin(true);
        SetNameType(listBtnType[currentIndex].GetNameType());
    }

    public void GetListType(){
        for(int i=0; i<transform.childCount; i++){
            BtnPickType btnPickType = transform.GetChild(i).GetComponent<BtnPickType>();
            btnPickType.SetID(i);
            Button button = btnPickType.GetComponent<Button>();
            listBtnType.Add(btnPickType);
            listBtn.Add(button);
            int idx = i;
            button.onClick.AddListener(()=>PickType(idx));
        }
    }

    private void PickType(int id)
    {
        listBtnType[currentIndex].SetOutline(false);
        listBtnType[currentIndex].DisplayShopTypeSkin(false);
        currentIndex = id;
        listBtnType[currentIndex].SetOutline(true);
        listBtnType[currentIndex].DisplayShopTypeSkin(true);
        SetNameType(listBtnType[currentIndex].GetNameType());
    }
    public void SetNameType(string nameType){
        txtNameType.text = nameType;
    }
}
