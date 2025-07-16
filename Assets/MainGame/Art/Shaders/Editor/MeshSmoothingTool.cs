#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public class MeshSmoothingTool : EditorWindow
{
    private Mesh originalMesh;
    private float smoothingAngle = 60f;
    [Tooltip("Vertices closer than this distance will be welded.")]
    private float distanceThreshold = 0.01f;
    private float edgeThreshold = 0.01f;

    [MenuItem("Tools/Mesh Smoothing Tool")]
    public static void ShowWindow()
    {
        GetWindow<MeshSmoothingTool>("Mesh Smoothing Tool");
    }

    private void OnGUI()
    {
        GUILayout.Label("Select a Mesh .asset File", EditorStyles.boldLabel);

        originalMesh = (Mesh)EditorGUILayout.ObjectField("Mesh Asset", originalMesh, typeof(Mesh), false);
        smoothingAngle = EditorGUILayout.Slider("Smoothing Angle", smoothingAngle, 0f, 180f);
        distanceThreshold = EditorGUILayout.FloatField(distanceThreshold);
        edgeThreshold = EditorGUILayout.FloatField(edgeThreshold);

        if (originalMesh && GUILayout.Button("Generate Smoothed Mesh"))
        {
            GenerateSmoothedMesh();
        }

        if (originalMesh && GUILayout.Button("Weld and Save New Mesh"))
        {
            WeldAndSaveMesh();
        }
    }
    void WeldAndSaveMesh()
    {
        Mesh welded = WeldAndSnapToEdges(originalMesh, distanceThreshold, edgeThreshold);

        string path = AssetDatabase.GetAssetPath(originalMesh);
        string dir = Path.GetDirectoryName(path);
        string newPath = Path.Combine(dir, originalMesh.name + $"_WeldedEdge.asset");

        AssetDatabase.CreateAsset(welded, newPath);
        AssetDatabase.SaveAssets();
        Debug.Log("✅ Welded & edge-snapped mesh saved to: " + newPath);
    }

    Mesh WeldAndSnapToEdges(Mesh mesh, float vertexThreshold, float edgeThreshold)
    {
        Vector3[] oldVerts = mesh.vertices;
        Vector3[] oldNormals = mesh.normals;
        Vector2[] oldUVs = mesh.uv;
        int[] oldTris = mesh.triangles;

        List<Vector3> newVerts = new List<Vector3>();
        List<Vector3> newNormals = new List<Vector3>();
        List<Vector2> newUVs = new List<Vector2>();

        Dictionary<int, int> vertMap = new Dictionary<int, int>();

        // Step 1: Vertex-to-vertex merge
        for (int i = 0; i < oldVerts.Length; i++)
        {
            Vector3 v = oldVerts[i];
            int found = -1;
            for (int j = 0; j < newVerts.Count; j++)
            {
                if (Vector3.SqrMagnitude(newVerts[j] - v) < vertexThreshold * vertexThreshold)
                {
                    found = j;
                    break;
                }
            }

            if (found == -1)
            {
                vertMap[i] = newVerts.Count;
                newVerts.Add(v);
                newNormals.Add(oldNormals.Length > i ? oldNormals[i] : Vector3.up);
                newUVs.Add(oldUVs.Length > i ? oldUVs[i] : Vector2.zero);
            }
            else
            {
                vertMap[i] = found;
            }
        }

        // Step 2: Snap to edge if not part of any triangle
        for (int i = 0; i < oldVerts.Length; i++)
        {
            int mappedIndex = vertMap[i];
            Vector3 v = oldVerts[i];

            bool isOnEdge = false;
            for (int t = 0; t < oldTris.Length; t += 3)
            {
                int i0 = vertMap[oldTris[t]];
                int i1 = vertMap[oldTris[t + 1]];
                int i2 = vertMap[oldTris[t + 2]];
                if (mappedIndex == i0 || mappedIndex == i1 || mappedIndex == i2)
                {
                    isOnEdge = true;
                    break;
                }
            }

            if (isOnEdge) continue;

            for (int t = 0; t < oldTris.Length; t += 3)
            {
                int[] ids = {
                    vertMap[oldTris[t]],
                    vertMap[oldTris[t + 1]],
                    vertMap[oldTris[t + 2]]
                };

                for (int e = 0; e < 3; e++)
                {
                    int a = ids[e];
                    int b = ids[(e + 1) % 3];

                    Vector3 va = newVerts[a];
                    Vector3 vb = newVerts[b];

                    float dist;
                    Vector3 projected = ProjectPointToSegment(v, va, vb, out dist);

                    if (dist < edgeThreshold)
                    {
                        // Interpolate UVs and normals
                        Vector2 uva = newUVs[a];
                        Vector2 uvb = newUVs[b];
                        Vector3 na = newNormals[a];
                        Vector3 nb = newNormals[b];

                        float tParam = Vector3.Dot(projected - va, (vb - va)) / (vb - va).sqrMagnitude;
                        tParam = Mathf.Clamp01(tParam);

                        Vector2 uv = Vector2.Lerp(uva, uvb, tParam);
                        Vector3 n = Vector3.Lerp(na, nb, tParam).normalized;

                        vertMap[i] = newVerts.Count;
                        newVerts.Add(projected);
                        newUVs.Add(uv);
                        newNormals.Add(n);

                        goto NextVertex;
                    }
                }
            }

        NextVertex: continue;
        }

        // Step 3: Build new triangles
        List<int> newTris = new List<int>();
        for (int i = 0; i < oldTris.Length; i++)
        {
            newTris.Add(vertMap[oldTris[i]]);
        }

        Mesh newMesh = new Mesh();
        newMesh.name = mesh.name + "_WeldedEdge";
        newMesh.vertices = newVerts.ToArray();
        newMesh.normals = newNormals.ToArray();
        newMesh.uv = newUVs.ToArray();
        newMesh.triangles = newTris.ToArray();
        newMesh.RecalculateBounds();

        return newMesh;
    }

    // Projects a point onto a line segment and returns the closest point + distance
    Vector3 ProjectPointToSegment(Vector3 p, Vector3 a, Vector3 b, out float distance)
    {
        Vector3 ab = b - a;
        float t = Vector3.Dot(p - a, ab) / ab.sqrMagnitude;
        t = Mathf.Clamp01(t);
        Vector3 projection = a + ab * t;
        distance = Vector3.Distance(p, projection);
        return projection;
    }
    
    /// <summary>
    /// Start Smooth
    /// </summary>
    

    void GenerateSmoothedMesh()
    {
        if (originalMesh == null)
        {
            Debug.LogError("No mesh selected.");
            return;
        }

        Mesh newMesh = Object.Instantiate(originalMesh);

        // Recalculate smooth normals based on angle
        RecalculateNormalsBySmoothingAngle(newMesh, smoothingAngle);

        // Save the new mesh
        string originalPath = AssetDatabase.GetAssetPath(originalMesh);
        string dir = Path.GetDirectoryName(originalPath);
        string name = Path.GetFileNameWithoutExtension(originalPath);
        string newPath = Path.Combine(dir, $"{name}_Smoothed_{(int)smoothingAngle}.asset");

        AssetDatabase.CreateAsset(newMesh, newPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log($"✅ Smoothed mesh saved at: {newPath}");
    }

    void RecalculateNormalsBySmoothingAngle(Mesh mesh, float angleThreshold)
    {
        float cosThreshold = Mathf.Cos(angleThreshold * Mathf.Deg2Rad);

        Vector3[] vertices = mesh.vertices;
        int[] triangles = mesh.triangles;
        Vector3[] faceNormals = new Vector3[triangles.Length / 3];
        Vector3[] normals = new Vector3[vertices.Length];

        // Map from vertex to all face indices it belongs to
        Dictionary<Vector3, List<int>> vertexToFaces = new Dictionary<Vector3, List<int>>();

        // Calculate face normals
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int i0 = triangles[i];
            int i1 = triangles[i + 1];
            int i2 = triangles[i + 2];

            Vector3 v0 = vertices[i0];
            Vector3 v1 = vertices[i1];
            Vector3 v2 = vertices[i2];

            Vector3 normal = Vector3.Cross(v1 - v0, v2 - v0).normalized;
            faceNormals[i / 3] = normal;

            AddFace(vertexToFaces, v0, i / 3);
            AddFace(vertexToFaces, v1, i / 3);
            AddFace(vertexToFaces, v2, i / 3);
        }

        // Smooth normals per vertex
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = vertices[i];
            List<int> connectedFaces = vertexToFaces[vertex];

            Vector3 smoothNormal = Vector3.zero;
            Vector3 currentFaceNormal = faceNormals[FindFaceIndex(triangles, i)];

            foreach (int faceIndex in connectedFaces)
            {
                Vector3 n = faceNormals[faceIndex];
                if (Vector3.Dot(n, currentFaceNormal) >= cosThreshold)
                {
                    smoothNormal += n;
                }
            }

            normals[i] = smoothNormal.normalized;
        }

        mesh.normals = normals;
    }

    void AddFace(Dictionary<Vector3, List<int>> dict, Vector3 vertex, int faceIndex)
    {
        if (!dict.ContainsKey(vertex))
            dict[vertex] = new List<int>();
        dict[vertex].Add(faceIndex);
    }

    int FindFaceIndex(int[] triangles, int vertexIndex)
    {
        for (int i = 0; i < triangles.Length; i += 3)
        {
            if (triangles[i] == vertexIndex ||
                triangles[i + 1] == vertexIndex ||
                triangles[i + 2] == vertexIndex)
                return i / 3;
        }
        return 0;
    }
}
#endif