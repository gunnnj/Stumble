using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float timeStop = 1.5f;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(Slow(timeStop));
    }

    void Update()
    {

        rectTransform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);

    }

    public IEnumerator Slow(float time){
        yield return new WaitForSeconds(5f);

        float unit = rotationSpeed/(time/0.05f);
        while(time>0){
            yield return new WaitForSeconds(0.1f);
            time-=0.05f;
            rotationSpeed -= unit;
        }
        rotationSpeed = 0;
    }
}
