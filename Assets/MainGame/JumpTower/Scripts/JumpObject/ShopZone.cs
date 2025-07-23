using UnityEngine;

public class ShopZone : MonoBehaviour
{
    [SerializeField] GameObject go;

    void Start()
    {
        // btnShop.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){

            go.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")){

            go.SetActive(false);
        }
    }
}
