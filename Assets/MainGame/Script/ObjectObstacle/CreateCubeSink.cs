using System.Linq;
using UnityEngine;

public class CreateCubeSink : MonoBehaviour
{
    [SerializeField] GameObject[] prefabCubeSink;
    [SerializeField] int nCol = 5;
    [SerializeField] float distance = 2f;
    // private int[,] matrix;

    void Start()
    {
        SpawnCube();
    }

    public void SpawnCube(){
        for(int i=0; i<nCol; i++){
            for(int j=0; j<nCol; j++){
                Vector3 vector = transform.position + new Vector3(i*distance,0,j*distance);
                int ran = Random.Range(0,prefabCubeSink.Count());
                Instantiate(prefabCubeSink[ran],vector,Quaternion.identity);
            }
        }
    }

}
