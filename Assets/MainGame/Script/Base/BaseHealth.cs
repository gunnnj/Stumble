using System.Threading.Tasks;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IHealth
{
    [SerializeField] protected ParticleSystem bangEffect;
    protected ShooterModeUI shooterModeUI;
    protected int MaxHp;
    protected int CurrentHp;
    protected int Armor;
    protected Vector3 RevidePosition;
    protected ParticleSystem effectDead;

    protected virtual void Start()
    {
        shooterModeUI = FindFirstObjectByType<ShooterModeUI>();
        MaxHp = 100;
        RevidePosition = transform.position;
        ResetHealth();
        GameObject effect = Instantiate(bangEffect.gameObject,transform);
        effect.SetActive(false);
        effectDead = effect.GetComponent<ParticleSystem>();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet")){
            TakeDame(other.GetComponent<Bullet>().dame);
        }
    }
    protected void SpawnEffect(){
        effectDead.transform.position = transform.position;
        effectDead.gameObject.SetActive(true);
        effectDead.Play();
    }
    protected void ResetHealth(){
        CurrentHp = 100;
        Armor = 100;
    }
    public async void Dead()
    {
        SpawnEffect();
        await Task.Delay(200);
        Revide();
    }

    public void TakeDame(int dame)
    {
        if(Armor>0) Armor -= dame;
        else{
            CurrentHp -= dame;
            if(CurrentHp<=0){
                Dead();
            }
        }
        UpdateHealthUI(Armor,CurrentHp);
    }
    protected virtual void Revide(){
        ResetHealth();
    }
    protected virtual void UpdateHealthUI(int CurrentArmor, int CurrentHp){

    }
}
