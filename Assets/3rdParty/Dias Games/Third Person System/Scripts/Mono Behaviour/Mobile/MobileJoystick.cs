using UnityEngine;
using UnityEngine.EventSystems;

namespace DiasGames.Mobile
{
    public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject player;
        public float m_MoveRange = 100;
        public bool IsCameraJoystick = false;

        [Space]
        public bool InvertX = false;
        public bool InvertY = false;

        private Touch m_CurrentTouch;

        private Canvas m_Canvas;
        private Vector2 targetPoint;
        private Vector2 initialTouchPosition;

        [Space]
        [SerializeField] private RectTransform m_BackgroundRect = null;
        [SerializeField] private RectTransform m_HandleStickRect = null;
        [SerializeField] private FloatingJoystick floatingJoystick;

        Vector2 currentLocalPoint;
        Vector2 initialLocalPoint;
        Vector2 old;
        float x;
        float y;

        protected void Awake()
        {
            m_Canvas = GetComponentInParent<Canvas>();

            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player");
        }

        private void Start()
        {
            Vector2 center = new Vector2(0.5f, 0.5f);
            m_BackgroundRect.pivot = center;
            m_HandleStickRect.anchorMin = center;
            m_HandleStickRect.anchorMax = center;
            m_HandleStickRect.pivot = center;
            m_HandleStickRect.anchoredPosition = Vector2.zero;
        }

        private void Update()
        {
            x = Mathf.Clamp(targetPoint.x, -1, 1) * (InvertX ? -1 : 1);

            if (IsCameraJoystick){
                if(old!=currentLocalPoint){
                    y = Mathf.Clamp(targetPoint.y, -1, 1) * (InvertY ? 1 : -1);
                    player.SendMessage("OnLook", new Vector2(x, y));
                    // if(old.x>currentLocalPoint.x){
                    //     Debug.Log(">");
                    // }
                    // else{
                    //     Debug.Log("<");
                    // }
                    old = currentLocalPoint;
                    
                }
                else{
                    player.SendMessage("OnLook", new Vector2(0, 0));
                    initialLocalPoint = currentLocalPoint;
                    
                }
                    
                
            }
            else{
                x = floatingJoystick.Horizontal;
                y = floatingJoystick.Vertical;
                player.SendMessage("OnMove", new Vector2(x, y));
            }
                
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            //Origin code
            // OnDrag(eventData);

            // Lưu vị trí chạm ban đầu
            initialTouchPosition = eventData.position;
            targetPoint = Vector2.zero;

            // Chuyển điểm chạm sang tọa độ cục bộ tương đối với m_BackgroundRect
            Camera cam = null;
            if (m_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
                cam = m_Canvas.worldCamera;

            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_BackgroundRect, eventData.position, cam, out localPoint);
            m_HandleStickRect.anchoredPosition = localPoint / m_Canvas.scaleFactor;

            
        }

        public void OnDrag(PointerEventData eventData)
        {

            Camera cam = null;
            if (m_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
                cam = m_Canvas.worldCamera;

            Vector2 radius = m_BackgroundRect.sizeDelta / 2;

            
            targetPoint = (eventData.position - initialTouchPosition) / (radius * m_Canvas.scaleFactor);

            if (targetPoint.magnitude > 1)
                targetPoint.Normalize();

    
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_BackgroundRect, eventData.position, cam, out currentLocalPoint);
            // currentLocalPoint = eventData.position;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_BackgroundRect, initialTouchPosition, cam, out initialLocalPoint);
            Vector2 offset = (currentLocalPoint - initialLocalPoint) / m_Canvas.scaleFactor;
            if (offset.magnitude > m_MoveRange)
                offset = offset.normalized * m_MoveRange;
            m_HandleStickRect.anchoredPosition = (initialLocalPoint / m_Canvas.scaleFactor) + offset;
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            targetPoint = Vector2.zero;
            m_HandleStickRect.anchoredPosition = Vector2.zero;
        }
    }
}
