using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkin : MonoBehaviour
{
    private int idItem;
    private Outline outline;
    private Button button;

    void Start()
    {
        outline = GetComponent<Outline>();
        button = GetComponent<Button>();
        outline.enabled = false;
        button.onClick.AddListener(PickSkin);
    }

    private void PickSkin()
    {
        Debug.Log("PickSkin");
    }

    public void SetOutline(bool value){
        outline.enabled = value;
    }
    public void SetID(int value){
        idItem = value;
    }
    public int GetID(){
        return idItem;
    }


}
