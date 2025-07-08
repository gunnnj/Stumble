
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnBlock : MonoBehaviour
{
    [SerializeField] MoveToTarget[] listBlock;
    [SerializeField] Transform target;
    [SerializeField] float timeSpawn = 3f;
    private Coroutine coroutine;

    void OnEnable()
    {
        GameEvent.eventWinGame+=StopCoroutin;
        GameEvent.eventLoseGame+=StopCoroutin;
    }
    void OnDisable()
    {
        GameEvent.eventWinGame-=StopCoroutin;
        GameEvent.eventLoseGame-=StopCoroutin;
    }

    void Start()
    {
        SetTarget();
        // coroutine = StartCoroutine(RandomBlock());
        coroutine = StartCoroutine(SequenBlock());
    }
    
    public void SetTarget(){
        for(int i=0; i<listBlock.Count(); i++){
            listBlock[i].target = target;
            listBlock[i].isLoop = false;
            listBlock[i].gameObject.SetActive(false);
        }
    }
    public void SetPositionStart(int index){
        listBlock[index].transform.position = transform.position;
        listBlock[index].gameObject.SetActive(true);
        listBlock[index].isTarget = false;
    }

    public IEnumerator RandomBlock(){
        while(true){
            int ran = Random.Range(0,listBlock.Count()-1);
            if(listBlock[ran].gameObject.activeSelf) continue;
            SetPositionStart(ran);
            yield return new WaitForSeconds(timeSpawn);
        } 
    }
    public IEnumerator SequenBlock(){
        int id = 0;
        while(true){
            SetPositionStart(id);
            yield return new WaitForSeconds(timeSpawn);
            id++;
            if(id==listBlock.Count()-1){
                id = 0;
            }
        } 
    }
    private void StopCoroutin()
    {
        StopCoroutine(coroutine);
    }

}
