using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SnapToItem : MonoBehaviour
{
    public ScrollRect scrollRect;
    public RectTransform contentPanel;
    public RectTransform sampleListItem;
    public HorizontalLayoutGroup HLG;
    public TMP_Text nameLable;
    public string[] ItemNames;
    public float snapForce;
    private ScrollView scrollView;
    float snapSpeed;
    bool isSnapped;
    int currentItem;

    void Start()
    {
        scrollView = GetComponent<ScrollView>();
        isSnapped = false;
    }

    void Update()
    {
        currentItem = Mathf.RoundToInt(0 - contentPanel.localPosition.x / (sampleListItem.rect.width + HLG.spacing)); 
        if (scrollRect.velocity.magnitude < 200 && !isSnapped) 
        { 
            scrollRect.velocity = Vector2.zero; 
            snapSpeed += snapForce * Time.deltaTime;
            contentPanel.localPosition = new Vector3(
                Mathf.MoveTowards(contentPanel.localPosition.x, 0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)), snapSpeed), 
                contentPanel.localPosition.y, 
                contentPanel.localPosition.z); 
            
            if (Mathf.Abs(contentPanel.localPosition.x - (0 - (currentItem * (sampleListItem.rect.width + HLG.spacing)))) < 0.01f)
            {
                isSnapped = true; 
            }
            
        } 
        if(scrollRect.velocity.magnitude  > 200) {
            isSnapped = false; 
            snapSpeed = 0; 
        }
        
    }
    public void SnapToNearestItem()
    {
        Debug.Log("Snap");
        isSnapped = false; 
        scrollRect.velocity = Vector2.zero; 
        snapSpeed = 0; 
        Debug.Log(currentItem); 


        scrollView.ActiveMap(currentItem);
    }

}
