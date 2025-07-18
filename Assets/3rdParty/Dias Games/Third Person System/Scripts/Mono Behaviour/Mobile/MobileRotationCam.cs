using UnityEngine;
using UnityEngine.EventSystems;

public class MobileRotationCam : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private GameObject player;
    public float sensitivity = 1.0f;
    private float originSen;

    [Space]
    public bool InvertX = false;
    public bool InvertY = false;

    private Vector2 lastPointerPosition;
    Vector2 old;
    private bool isTouching = false;

    void Awake()
    {
        originSen = sensitivity;
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {

        if(old != lastPointerPosition){
            sensitivity = originSen;
        }
        else{
            sensitivity = 0;
        }
        old = lastPointerPosition;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isTouching)
            return;

        Vector2 delta = eventData.position - lastPointerPosition;
        lastPointerPosition = eventData.position;

        float x = delta.x * sensitivity * (InvertX ? -1 : 1);
        float y = delta.y * sensitivity * (InvertY ? -1 : 1);

        player.SendMessage("OnLook", new Vector2(x, y));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouching = true;
        lastPointerPosition = eventData.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Stop();
    }
    void Stop()
    {
        isTouching = false;

        player.SendMessage("OnLook", Vector2.zero);
    }
}
