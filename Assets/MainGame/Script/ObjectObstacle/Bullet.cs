using System.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speedFire = 100f;
    [SerializeField] GameObject effectPrefab;
    public int dame;
    private GameObject effect;
    // private Rigidbody rigidbody;
    bool isFire = false;
    [HideInInspector] public Vector3 endPoint;
    void Awake()
    {
        // rigidbody = GetComponent<Rigidbody>();
        effect = Instantiate(effectPrefab,gameObject.transform);
        effect.SetActive(false);
    }

    void OnEnable()
    {
        // rigidbody.useGravity = false;
    }
    async void Update()
    {
        if(!isFire) return;
        transform.position = Vector3.MoveTowards(transform.position,endPoint, speedFire*Time.deltaTime);
        if(Vector3.Distance(transform.position,endPoint)<0.05f){
            effect.SetActive(true);
            isFire = false;
            await Task.Delay(100);
            effect.SetActive(false);
            DisActive();
        }
    }
    public void SetInfo(Vector3 startP, Vector3 endP, TypeGun typeGun){
        transform.position = startP;
        endPoint = endP;
        isFire = true;
        switch (typeGun){
            case TypeGun.Light:
                dame = 30;
                break;
            case TypeGun.Medium:
                dame = 50;
                break;
            case TypeGun.Weight:
                dame = 70;
                break;
            default:
                break;
            
        }
        
    }
    public void DisActive(){
        gameObject.SetActive(false); 
    }
    private async void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            effect.SetActive(true);
            isFire = false;
            await Task.Delay(100);
            effect.SetActive(false);
            DisActive();
        }
        if(other.CompareTag("Ground")){
            effect.SetActive(true);
            isFire = false;
            await Task.Delay(100);
            effect.SetActive(false);
            DisActive();
        }
    }

}
public enum TypeGun{
    Light,
    Medium,
    Weight
}
