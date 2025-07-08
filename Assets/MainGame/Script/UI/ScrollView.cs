using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollView : MonoBehaviour
{

    public ScrollRect scrollRect;
    public RectTransform viewPort;
    public RectTransform contentPanel;
    public HorizontalLayoutGroup HLG;
    public RectTransform[] ItemList;
    public SnapToItem snapToItem;
    public List<ItemMap> listItemMap =  new List<ItemMap>();
    Vector2 OldVelocity;
    bool isUpdated;

    void Start()
    {
        snapToItem = GetComponent<SnapToItem>();

        isUpdated = false;
        OldVelocity = Vector2.zero;

        int ItemToAdd = Mathf.CeilToInt(viewPort.rect.width/(ItemList[0].rect.width+HLG.spacing));

        for(int i=0; i<ItemToAdd; i++){
            RectTransform RT = Instantiate(ItemList[i % ItemList.Length], contentPanel);
            RT.SetAsLastSibling();
        }
        for(int i=0; i<ItemToAdd; i++){
            int num = ItemList.Length - i -1;
            while(num<0){
                num+=ItemList.Length;

            }
            RectTransform RT = Instantiate(ItemList[num],contentPanel);
            RT.SetAsFirstSibling();
        }
        contentPanel.localPosition = new Vector3(0-(ItemList[0].rect.width+HLG.spacing)*ItemToAdd,
            contentPanel.localPosition.y,
            contentPanel.localPosition.z
        );

        for(int i=0; i<contentPanel.childCount; i++){
            listItemMap.Add(contentPanel.GetChild(i).GetComponent<ItemMap>());
        }

        Scroll();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(AutoScroll(5f)); 
        }

        if(isUpdated){
            isUpdated = false;
            scrollRect.velocity = OldVelocity;
        }

        if(contentPanel.localPosition.x>0){
            Canvas.ForceUpdateCanvases();
            OldVelocity = scrollRect.velocity;
            contentPanel.localPosition -= new Vector3(ItemList.Length * (ItemList[0].rect.width+HLG.spacing),0,0);
            isUpdated = true;
        }
        if(contentPanel.localPosition.x<0 - (ItemList.Length * (ItemList[0].rect.width+HLG.spacing))){
            Canvas.ForceUpdateCanvases();
            OldVelocity = scrollRect.velocity;
            contentPanel.localPosition += new Vector3(ItemList.Length * (ItemList[0].rect.width+HLG.spacing),0,0);
            isUpdated = true;
        }
    }
    public void Scroll(){
        StartCoroutine(AutoScroll(5f)); 
    }
    private IEnumerator AutoScroll(float duration)
    {
        float elapsedTime = 0f;
        float initialSpeed = Random.Range(9000,10000f); 
        float currentSpeed = initialSpeed;

        while (elapsedTime < duration)
        {
            contentPanel.localPosition -= new Vector3(currentSpeed * Time.deltaTime, 0, 0);
            elapsedTime += Time.deltaTime;

            currentSpeed = Mathf.Lerp(initialSpeed, 0, elapsedTime / duration);

            yield return null;
        }

        contentPanel.localPosition -= new Vector3(currentSpeed * Time.deltaTime, 0, 0);

        scrollRect.velocity = Vector2.zero;

        if (snapToItem != null)
        {
            snapToItem.SnapToNearestItem();
            scrollRect.horizontal = false;

        }
    }
    public void ActiveMap(int id){
        foreach(var item in listItemMap){
            // if(item.index == id){
            //     item.TurnBtn();
            // }
        }
    }
    public void ResetListBtn(){
        foreach(var item in listItemMap)
        {
            // item.ResetStatus();
        }
        scrollRect.horizontal = true;
        // doubleArrow.SetActive(false);
        // singleArrow.SetActive(true);
    }
}
