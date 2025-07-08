using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerShootMode : MonoBehaviour
{
    [SerializeField] FloatingJoystick floatingJoystick;
    [SerializeField] Animator animator;
    [SerializeField] Camera mainCam;
    [SerializeField] GameObject gun;
    [SerializeField] float speed = 4f;
    [SerializeField] float rotateSpeed = 10f;
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float checkDistance = 0.1f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask layerShoot;
    [SerializeField] Transform bulletPool;
    [SerializeField] Transform pointShoot;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int initialPoolSize = 16;
    [SerializeField] GameObject[] gunGo;
    public TypeGun typeGun;
    private float rayDistance = 50f;
    private ShooterModeUI shooterModeUI;
    private Rigidbody rb;
    private Vector3 dir;
    private Vector3 lastCalculatedPoint;
    private GameObject bulletShoot;
    private List<GameObject> poolerBullet = new List<GameObject>();
    private const string AnimIdle = "isMove";
    private const string AnimVelX = "VelocityX";
    private const string AnimVelZ = "VelocityZ"; 

    void Awake()
    {
        InitializeBulletPool();
    }
    void Start()
    {
        shooterModeUI = FindFirstObjectByType<ShooterModeUI>();
        rb = GetComponent<Rigidbody>();
        shooterModeUI.onJump = ()=> Jump();
        shooterModeUI.onShoot = ()=> Shoot();
        PickGun(TypeGun.Light);
        // SetPowerByGun();

    }

    void Update()
    {
        Rotate();
        // float dirX = Input.GetAxis("Horizontal");
        // float dirZ = Input.GetAxis("Vertical");
        float dirX = floatingJoystick.Horizontal;
        float dirZ = floatingJoystick.Vertical;
        // Debug.Log("dir X: "+dirX);
        // Debug.Log("dir Z:"+ dirZ);

        dir = new Vector3(dirX,0,dirZ);

        if(dir.magnitude>0.1f){
            animator.SetBool(AnimIdle, true);
            ControlAnimation(dirX,dirZ);
            Move();
        }
        else{
            animator.SetBool(AnimIdle, false);
            animator.SetFloat(AnimVelX, 0);
            animator.SetFloat(AnimVelZ, 0);
        }
        // if(Input.GetMouseButtonDown(0)){
        //     Shoot();
        // }
        if(Input.GetKeyDown(KeyCode.Space)){
            Jump();
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
    public void ControlAnimation(float X, float Z){
        // if(Z>0){
        //     animator.SetFloat(AnimVelX, 1);
        // }
        // else if(Z==0){
        //     animator.SetFloat(AnimVelX, 0);
        // }
        // else{
        //     animator.SetFloat(AnimVelX, -1);
        // }
        // if(X>0){
        //     animator.SetFloat(AnimVelZ, 1);
        // }
        // else if(X==0){
        //     animator.SetFloat(AnimVelZ, 0);
        // }
        // else{
        //     animator.SetFloat(AnimVelZ, -1);
        // }

        float speedChange = 5f;

        // float targetVelX = X > 0 ? 1 : (X < 0 ? -1 : 0);
        // float targetVelZ = Z > 0 ? 1 : (Z < 0 ? -1 : 0);

        float targetVelX = Z > 0 ? 1 : (Z < 0 ? -1 : 0);
        float targetVelZ = X > 0 ? 1 : (X < 0 ? -1 : 0);

        animator.SetFloat(AnimVelX, Mathf.MoveTowards(animator.GetFloat(AnimVelX), targetVelX, speedChange * Time.deltaTime));
        animator.SetFloat(AnimVelZ, Mathf.MoveTowards(animator.GetFloat(AnimVelZ), targetVelZ, speedChange * Time.deltaTime));

    }
    private void Jump()
    {
        if(!IsGrounded()) return;
        rb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);
        Debug.Log("Jump");
    }
    private void InitializeBulletPool()
    {
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPool);
            bullet.SetActive(false);
            poolerBullet.Add(bullet); 

        }
    }

    public void PickGun(TypeGun type){
        typeGun = type;
        Debug.Log("Pick gun "+type.ToString());
        DisplayGun(type);
        switch (typeGun){
            case TypeGun.Light:
                speed = 5f;
                rayDistance = 50f;
                break;
            case TypeGun.Medium:
                speed = 4.2f;
                rayDistance = 40f;
                break;
            case TypeGun.Weight:
                speed = 3.5f;
                rayDistance = 30f;
                break;
            default:
                break;
            
        }
    }

    private void Shoot()
    {
        lastCalculatedPoint = GetRayEndPoint();

        if (lastCalculatedPoint != Vector3.zero) 
        {
            Debug.DrawLine(mainCam.transform.position, lastCalculatedPoint, Color.yellow, 1f);
        }

        
        //Rotation gun
        Vector3 dirGun = (lastCalculatedPoint - pointShoot.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(dirGun);
        Vector3 eulerAngles = targetRotation.eulerAngles;
        eulerAngles.y = gun.transform.eulerAngles.y; 
        gun.transform.rotation = Quaternion.Euler(eulerAngles);




        Debug.DrawLine(pointShoot.position, lastCalculatedPoint, Color.red, 1f);

        bulletShoot = GetBullet();
        bulletShoot.GetComponent<Bullet>().SetInfo(pointShoot.position,lastCalculatedPoint, typeGun);
        bulletShoot.SetActive(true);

        AudioShooterMode.Instance.PlaySFX(AudioClips.LaserShoot);

    }


    public GameObject GetBullet(){
        foreach(var item in poolerBullet){
            if(!item.activeSelf) return item;
        }
        GameObject bullet = Instantiate(bulletPrefab, bulletPool);
        bullet.SetActive(false);
        poolerBullet.Add(bullet);
        return bullet;
    }

    public Vector3 GetRayEndPoint(){
        Vector3 rayOrigin = mainCam.transform.position;
        Vector3 rayDirection = mainCam.transform.forward;

        Ray ray = new Ray(rayOrigin, rayDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance,layerShoot))
        {
            return hit.point;
        }
        else
        {
            return rayOrigin + rayDirection * rayDistance;
        }
    }

    private void Rotate()
    {
        Vector3 dirLook = mainCam.transform.forward;
        dirLook.y = 0;
        Vector3 rotate = Vector3.RotateTowards(transform.forward,dirLook,rotateSpeed*Time.deltaTime,0f);
        transform.rotation = Quaternion.LookRotation(rotate);
    }

    private void Move()
    {
        Vector3 inputDirection = dir.normalized;
        Vector3 moveDirection = transform.TransformDirection(inputDirection);
        transform.position += moveDirection * speed * Time.deltaTime;
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, checkDistance, groundLayer);
        // Vector3 checkPosition = transform.position - new Vector3(0, checkDistance, 0);

        // return Physics.CheckSphere(checkPosition, checkDistance, groundLayer);
    }
}
