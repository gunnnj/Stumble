using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class BotShooterMode : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject gun;
    [SerializeField] Transform bulletPool;
    [SerializeField] Transform pointShoot;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float navMeshSampleRadius = 1f;
    [SerializeField] public TagTarget tagTarget;
    [HideInInspector] public NavMeshAgent agent;
    [SerializeField] GameObject[] gunGo;
    public TypeGun typeGun;
    private SphereCollider sphereCollider;
    private Rigidbody rb;
    private Vector3 randomDirection;
    private Vector3 newPosition;
    private bool isTarget = false;
    // private bool isJumping = false;
    private List<Bullet> listBullet = new List<Bullet>();
    private const string AnimIdle = "isIdle";

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        SetNewPos();
        GetBulletPooling();
        SetPowerByGun();
    }
    void Update()
    {
        if(Vector3.Distance(transform.position,newPosition)<1.6f){
            SetNewPos();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(tagTarget.ToString())) isTarget = true;
    }
    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag(tagTarget.ToString())){
            if(isTarget){
                animator.SetBool(AnimIdle,true);
                isTarget = false;
                agent.enabled = false;
                //Code lại xử lý quay
                RotationToTarget(other.transform.position);
                StartCoroutine(ThreeShoot(other.transform.position));

            }
        
        }
    }
    public void DisplayGun(TypeGun typeGun){
        for(int i =0; i< gunGo.Count(); i++){
            if(i==(int)typeGun){
                gunGo[i].SetActive(true);
            }
            else{
                gunGo[i].SetActive(false);
            }
        }
    }
    // [ContextMenu("Jump")]
    // public void Jump()
    // {
    //     if (!isJumping) // Chỉ cho phép nhảy nếu không đang nhảy
    //     {
    //         agent.enabled = false; // Vô hiệu hóa NavMeshAgent trước khi nhảy
    //         rb.isKinematic = false; // Đảm bảo Rigidbody không phải là kinematic
    //         rb.useGravity = true;
    //         rb.AddForce(Vector3.up * 7f, ForceMode.Impulse);
    //         isJumping = true;
    //         StartCoroutine(WaitForJumpToEnd()); // Bắt đầu coroutine để theo dõi cú nhảy
    //     }
    // }

    // IEnumerator WaitForJumpToEnd()
    // {

    //     yield return new WaitForSeconds(1.5f); 
    //     rb.useGravity = false;
    //     rb.isKinematic = true;
    //     agent.enabled = true; 
    //     SetNewPos(); 
    //     isJumping = false;
        
    // }
    public void RandomTypeGun(){
        int type = Random.Range(0,2);
        typeGun = (TypeGun)type;
    }
    public void SetPowerByGun(){
        RandomTypeGun();
        DisplayGun(typeGun);
        switch (typeGun){
            case TypeGun.Light:
                sphereCollider.radius = 14;
                agent.speed = 5f;
                break;
            case TypeGun.Medium:
                sphereCollider.radius = 12;
                agent.speed = 4f;
                break;
            case TypeGun.Weight:
                sphereCollider.radius = 10;
                agent.speed = 3.5f;
                break;
            default:
                break;
            
        }
    }
    public void GetBulletPooling(){
        for(int i=0; i<bulletPool.childCount; i++){
            listBullet.Add(bulletPool.GetChild(i).GetComponent<Bullet>());
        }
    }
    public void SetNewPos(){
        newPosition = RandomMove();
        agent.SetDestination(newPosition);
    }

    public Vector3 RandomMove(){
        NavMeshHit hit;
        Vector3 newPos ;
        do
        {
            randomDirection = Random.insideUnitSphere.normalized * RandomDistance();
            newPos = transform.position + randomDirection;
        } while (!NavMesh.SamplePosition(newPos, out hit, navMeshSampleRadius, NavMesh.AllAreas));

        return hit.position;
    }
    public float RandomDistance(){
        return Random.Range(5,15);
    }

    public IEnumerator ThreeShoot(Vector3 target){
        int randomShoot  = Random.Range(2,6);
        while(randomShoot>0){
            Shoot(RandomPosShoot(target));
            yield return new WaitForSeconds(0.5f);
            randomShoot--;
        }
        // Shoot(RandomPosShoot(target));
        // yield return new WaitForSeconds(0.5f);
        // Shoot(RandomPosShoot(target));
        // yield return new WaitForSeconds(0.5f);
        // Shoot(RandomPosShoot(target));
        // yield return new WaitForSeconds(0.5f);
        agent.enabled = true;
        animator.SetBool(AnimIdle,false);
        SetNewPos();
    }
    public Vector3 RandomPosShoot(Vector3 target){
        float radius = 1.5f;
        Vector3 randomOffset = new Vector3(Random.Range(-radius, radius), 
                                            Random.Range(-radius, radius), 
                                            Random.Range(-radius, radius));
        Vector3 newPosition = randomOffset + target;

        return newPosition;
    }

    private void RotationToTarget(Vector3 target)
    {
        Vector3 newDir = (target-transform.position).normalized;
        newDir.y = 0;
        Quaternion quaternion = Quaternion.LookRotation(newDir);
        Vector3 euler = quaternion.eulerAngles;
        transform.rotation = Quaternion.Euler(euler);
    }

    private void Shoot(Vector3 target)
    {
        foreach(var item in listBullet){
            if(!item.gameObject.activeSelf){
                item.SetInfo(pointShoot.position,target,typeGun);
                item.gameObject.SetActive(true);
                AudioShooterMode.Instance.PlaySFX(AudioClips.LaserShoot);
                return;
            }
        }
    }
}
public enum TagTarget{
    Player,
    Bot
}

