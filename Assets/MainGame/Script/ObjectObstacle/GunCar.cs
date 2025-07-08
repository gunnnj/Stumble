using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class GunCar : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform target;
    [SerializeField] Transform pool;
    [SerializeField] float fireForce = 700f;
    [SerializeField] float timeRate = 4f;
    [SerializeField] int timeDestroyBullet = 5;
    public bool isRandomForce = false;
    List<GameObject> poolingBullet = new List<GameObject>();
    Vector3 dir;
    bool isActive;

    void OnEnable()
    {
        isActive = true;
    }
    void OnDisable()
    {
        isActive = false;
    }
    

    void Start()
    {

        dir = (target.position-transform.position).normalized;
        StartCoroutine(FireRateTime());
    }

    public IEnumerator FireRateTime(){
        while(true){
            FireBullet();
            yield return new WaitForSeconds(timeRate);
        }
    }
    public float RandomForce(){
        return Random.Range(fireForce-200,fireForce-50);
    }
    [ContextMenu("Fire")]
    public async void FireBullet(){
        GameObject bull;
        if(GetBulletPool()==null){
            bull = Instantiate(bullet,transform.position,Quaternion.identity, pool);

            Rigidbody rb = bull.GetComponent<Rigidbody>();
            if(!isRandomForce){
                rb.AddForce(dir*fireForce, ForceMode.Impulse);
            }else{
                rb.AddForce(dir*RandomForce(), ForceMode.Impulse);
            }
            

            await Task.Delay(timeDestroyBullet*1000);
            if(isActive){
                bull.SetActive(false);
                poolingBullet.Add(bull);
            }
            
        }
        else{
            bull = GetBulletPool();
            bull.transform.position = transform.position;
            bull.SetActive(true);
            Rigidbody rb = bull.GetComponent<Rigidbody>();

            // rb.AddForce(dir*fireForce, ForceMode.Impulse);
            if(!isRandomForce){
                rb.AddForce(dir*fireForce, ForceMode.Impulse);
            }else{
                rb.AddForce(dir*RandomForce(), ForceMode.Impulse);
            }

            await Task.Delay(timeDestroyBullet*1000);
            if(isActive){
                bull.SetActive(false);
            }
            
        }
        
    }

    public GameObject GetBulletPool(){
        foreach(var item in poolingBullet){
            if(!item.activeSelf) return item;
        }
        return null;
    }


    
}
