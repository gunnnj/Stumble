using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnPickType : MonoBehaviour
{
    public int IDtype;
    public GameObject scrollView;
    public Transform content;
    public int currentIndex=0;
    public List<ItemSkin> listItem = new List<ItemSkin>();
    public List<Button> listBtn = new List<Button>();
    public string nameType;
    private Outline outline;

    void Start()
    {
        scrollView = transform.Find("Scroll View").gameObject;
        content = transform.Find("Scroll View/Viewport/Content");

        outline = GetComponent<Outline>();
        scrollView.SetActive(false);
        outline.enabled = false;
        GetListItem();
    }

    public void GetListItem(){
        for(int i=0; i<content.childCount; i++){
            ItemSkin item = content.GetChild(i).GetComponent<ItemSkin>();
            Button button = item.GetComponent<Button>();
            item.SetID(i);
            listItem.Add(item);
            listBtn.Add(button);
            int index = i;
            button.onClick.AddListener(()=> PickSkinWithIdex(index));
        }
        
    }

    private void PickSkinWithIdex(int index)
    {
        listItem[currentIndex].SetOutline(false);
        currentIndex = index;
        listItem[currentIndex].SetOutline(true);
    }
    public void DisplayShopTypeSkin(bool value){
        scrollView.SetActive(value);
    }
    public void SetOutline(bool value){
        outline.enabled = value;
    }
    public void SetID(int value){
        IDtype = value;
    }
    public int GetID(){
        return IDtype;
    }
    public string GetNameType(){
        return nameType+"";
    }
}
