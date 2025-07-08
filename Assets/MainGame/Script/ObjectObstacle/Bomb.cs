using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] float timeExplosion = 4f;
    [SerializeField] GameObject bombPrefab;
    private GameObject effect;
    private SphereCollider sphereCollider;
    private Coroutine coroutine;

    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.enabled = false;
        // effect = Instantiate(bombPrefab,transform.parent);
        
    }

    void Update()
    {
        if(gameObject.activeSelf){
            coroutine = StartCoroutine(ExpByTime());
            
        }
    }
    [ContextMenu("Boom")]
    public async void Explosion(){
        // ManagerEffect.Instance.PlayEffect(ManagerEffect.Effect.bomb,transform.position);
        effect = Instantiate(bombPrefab,transform.position, Quaternion.identity);
        sphereCollider.enabled = true;
        await Task.Delay(100);
        gameObject.SetActive(false);
        sphereCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CanDestroy")){
            other.gameObject.SetActive(false);
            // Destroy(other.gameObject);
        }
    }
    public IEnumerator ExpByTime(){
        yield return new WaitForSeconds(timeExplosion);
        Explosion();
    }

}
