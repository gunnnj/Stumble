using UnityEngine;

public class ManagerEffect : MonoBehaviour
{
    public static ManagerEffect Instance;
    [SerializeField] ParticleSystem[] listEffect;

    void Awake()
    {
        Instance = this;
    }
    public void PlayEffect(Effect effect, Vector3 pos){
        listEffect[(int)effect].transform.position = pos;
        listEffect[(int)effect].gameObject.SetActive(true);
    }

    public enum Effect{
        bomb
    }

}
