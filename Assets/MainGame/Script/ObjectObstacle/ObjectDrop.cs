using System.Threading.Tasks;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    [SerializeField] public float timeDisactive = 6f;
    public Rigidbody rb;
    private Vector3 startPos;
    private Quaternion quaternion;
    private bool isActive = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        quaternion = transform.rotation;
    }
    void Start()
    {
        
    }
    async void OnEnable()
    {
        isActive = true;
        await Task.Delay((int)(timeDisactive*1000));
        if(isActive){
            gameObject.SetActive(false);
        }
        
    }
    private void OnDisable()
    {
        isActive = false; 
    }
    public void ResetPosition(){
        transform.position = startPos;
        transform.rotation = quaternion;
    }
    public void AddForce(float force, Vector3 dir){
        rb.AddForce(dir*force,ForceMode.Impulse);
    }
}
