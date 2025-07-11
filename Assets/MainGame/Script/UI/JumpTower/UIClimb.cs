using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIClimb : MonoBehaviour
{
    [SerializeField] Image arrow;
    [SerializeField] float minRectY;
    [SerializeField] float maxRectY;
    [SerializeField] float minPosY;
    [SerializeField] float maxPosY;
    [SerializeField] Transform player;
    [SerializeField] Slider slider;
    [SerializeField] TMP_Text txtGoldGet;
    [SerializeField] TMP_Text txtTotalGold;
    [Header("1 meter = ratio (coin)")]
    [SerializeField] float ratio = 100f;
    float rectY;
    float rectX;
    public float oldG;
    public float totalGold;
    public float g;

    void Start()
    {
        // arrow.rectTransform.DOAnchorPosY(maxPositionY, 2f);
        minRectY = arrow.rectTransform.position.y;
        rectX = arrow.rectTransform.position.x;
        oldG = 0;

    }
    void Update()
    {
        float playerY = player.position.y;

        // rectY = minRectY - (playerY/(maxPosY-minPosY))*(maxRectY-minRectY); 
        if(playerY>=minPosY){
            slider.value = playerY/(maxPosY-minPosY);
        }
        else{
            slider.value = 0;
            txtGoldGet.text = "0";
            if(oldG>1){
                totalGold += oldG*ratio;
                float goldtext = totalGold / 1000;
                txtTotalGold.text = goldtext.ToString("F2")+"K";
            }
            
            oldG = 0;
        }
        
        // Cập nhật vị trí của arrow (chỉ thay đổi Y, giữ nguyên X và Z)
        arrow.rectTransform.position = new Vector3(rectX, rectY, 0);

        UpdateGoldText(playerY);
    }

    public void UpdateGoldText(float positionY){
        if(positionY>=minPosY){
            g = (int)(positionY);
            if (g > oldG)
            {
                // Tính số vàng tương ứng
                float gold = g * ratio;

                // Hiển thị số vàng
                if (gold >= 1000)
                {
                    gold /= 1000;
                    txtGoldGet.text = gold.ToString("F2") + "K"; // Hiển thị với 1 chữ số thập phân
                }
                else
                {
                    txtGoldGet.text = gold.ToString("F0"); // Hiển thị số nguyên
                }

                // Cập nhật oldG
                oldG = g;
            }
            
        }
    }
}
