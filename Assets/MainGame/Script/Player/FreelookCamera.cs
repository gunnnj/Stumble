using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FreelookCamera : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    Image imgCam;
    [SerializeField] private CinemachineOrbitalFollow orbitalFollow;
    private Vector2 lastTouchPosition;
    [SerializeField] float touchSensitivityX = 0.1f; 
    [SerializeField] float touchSensitivityY = 0.1f;
    void Start()
    {
        imgCam = GetComponent<Image>();
          
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            imgCam.rectTransform,
            eventData.position,
            eventData.enterEventCamera,
            out Vector2 posOut
        ))
        {
            Vector2 currentTouchPosition = eventData.position;
            Vector2 deltaTouch = currentTouchPosition - lastTouchPosition;

            orbitalFollow.HorizontalAxis.Value += deltaTouch.x * touchSensitivityX;
            if(orbitalFollow.VerticalAxis.Value < orbitalFollow.VerticalAxis.Range[0]+2f){
                if(deltaTouch.y * touchSensitivityY < 0){
                    orbitalFollow.VerticalAxis.Value -= deltaTouch.y * touchSensitivityY;
                }
                else{
                    lastTouchPosition = currentTouchPosition;
                    return;
                }
            }
            if(orbitalFollow.VerticalAxis.Value > orbitalFollow.VerticalAxis.Range[1]-2f){
                if(deltaTouch.y * touchSensitivityY > 0){
                    orbitalFollow.VerticalAxis.Value -= deltaTouch.y * touchSensitivityY;
                }
                else{
                    lastTouchPosition = currentTouchPosition;
                    return;
                }
            }
            orbitalFollow.VerticalAxis.Value -= deltaTouch.y * touchSensitivityY;
            lastTouchPosition = currentTouchPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastTouchPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
