using System.Threading.Tasks;
using DiasGames.Controller;
using DiasGames.Mobile;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerSp : MonoBehaviour
{
    public MobileButton buttonJump;
    public CSPlayerController playerController;
    async void Start()
    {
        playerController = FindFirstObjectByType<CSPlayerController>();
        enabled = false;
        await Task.Delay(100);
        enabled = true;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (buttonJump != null)
            {
                // Tạo một PointerEventData
                PointerEventData pointerData = new PointerEventData(EventSystem.current)
                {
                    // Bạn có thể thiết lập các thuộc tính nếu cần
                };

                // Gọi phương thức OnPointerDown
                buttonJump.OnPointerDown(pointerData);
            }
            else
            {
                Debug.LogWarning("buttonJump is not assigned in the Inspector.");
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (buttonJump != null)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                buttonJump.OnPointerUp(pointerData);
            }
        }
    }
}
