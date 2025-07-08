using System.Threading.Tasks;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    [SerializeField] Material material;
    [SerializeField] float timeDestroy = 2f;
    [SerializeField] float timeLoop = 2f;
    public bool isLoop = false;
    private MeshRenderer meshRenderer;
    private Material originMat;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        originMat = meshRenderer.material;
    }
    async void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player")){
            meshRenderer.material = material;
        
            await Task.Delay((int)(timeDestroy*1000));
            gameObject.SetActive(false);
            if(isLoop){
                await Task.Delay((int)(timeLoop*1000));
                gameObject.SetActive(true);
                meshRenderer.material = originMat;
            }
        }
    }
}
