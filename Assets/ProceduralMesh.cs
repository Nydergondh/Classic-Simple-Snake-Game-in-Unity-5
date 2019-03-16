using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), (typeof(MeshRenderer)))] 
public class ProceduralMesh : MonoBehaviour
{

    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    [SerializeField] Vector3 gridOffset;
    [SerializeField] int gridSize;
    [SerializeField] float cellSize;
    float vertexOffset;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

    }

    // Update is called once per frame
    void Update()
    {
        MakeMeshData();
        CreateMesh();
    }

    void MakeMeshData() {

        vertices = new Vector3[4];
        triangles = new int[6];

        vertexOffset = cellSize * 0.5f;

        int v = 0; //vertices
        int t = 0; // triangles

        for (int l = 0; l < gridSize ;l++) {
            for (int c = 0; c < gridSize; c++) {
                vertices[v] = new Vector3(-vertexOffset, 0, -vertexOffset);
                vertices[v+1] = new Vector3(-vertexOffset, 0, vertexOffset);
                vertices[v + 2] = new Vector3(vertexOffset, 0, -vertexOffset);
                vertices[v + 3] = new Vector3(vertexOffset, 0, vertexOffset);

                triangles[t] = v;
                triangles[t+1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + 2;
                triangles[t + 5] = v + 3;
            }
        }
        
    }

    void CreateMesh() {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
