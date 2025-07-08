using UnityEngine;
using Unity.AI.Navigation;

public class NavmeshBuilder : MonoBehaviour
{
    private NavMeshSurface surface;

    void Start()
    {
        surface = GetComponent<NavMeshSurface>();
        BuildNavMesh();
    }
    void Update()
    {
        UpdateNavMesh();
    }

    public void BuildNavMesh()
    {
        surface.BuildNavMesh();
    }

    public void UpdateNavMesh()
    {
        surface.RemoveData();
        BuildNavMesh();
    }
    
}
