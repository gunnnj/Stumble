using UnityEngine;

public class ShopZone : MonoBehaviour
{
    [SerializeField] GameObject btnShop;

    void Start()
    {
        btnShop.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            Debug.Log("OpenShop");
            btnShop.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player")){
            Debug.Log("Close");
            btnShop.SetActive(false);
        }
    }
}
