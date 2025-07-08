using UnityEngine;

public class FolowTarget : MonoBehaviour
{
    [SerializeField] Transform target;
    public ParticleSystem effect;
    public bool isTurn = true;

    void Start()
    {
        effect = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        transform.position = target.position;
        // if(isTurn){
        //     effect.Play();
        // }
        // else{
        //     effect.Stop();
        // }
    }

    //Update position effect to fit player
}
