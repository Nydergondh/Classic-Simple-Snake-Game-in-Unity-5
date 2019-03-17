using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), (typeof(MeshRenderer)))] 
public class ProceduralMesh : MonoBehaviour
{

    Mesh mesh;
    Positions[] postions;

    private Vector3[] vertices;
    private int[] triangles;

    [SerializeField] Vector3 gridOffset;
    [SerializeField] int gridSize;
    [SerializeField] float cellSize;
    private float vertexOffset;
    
    

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

    public void MakeMeshData() {

        vertices = new Vector3[gridSize * gridSize * 4];
        triangles = new int[gridSize * gridSize * 6];
        postions = new Positions[gridSize * gridSize];

        vertexOffset = cellSize * 0.5f;

        int v = 0; //vertices
        int t = 0; // triangles

        for (int l = 0, posCount = 1; l < gridSize ;l++) {
            for (int c = 0; c < gridSize; c++) {

                Vector3 cellOffset = new Vector3(l * cellSize, 0, c * cellSize);

                vertices[v] = new Vector3(-vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                vertices[v+1] = new Vector3(-vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;
                vertices[v + 2] = new Vector3(vertexOffset, 0, -vertexOffset) + cellOffset + gridOffset;
                vertices[v + 3] = new Vector3(vertexOffset, 0, vertexOffset) + cellOffset + gridOffset;

                triangles[t] = v;
                triangles[t+1] = triangles[t + 4] = v + 1;
                triangles[t + 2] = triangles[t + 3] = v + 2;
                triangles[t + 5] = v + 3;

                v += 4;
                t += 6;

                postions[posCount].QuadPosition = posCount; //create the grid while adding a value to the postion variable.
                posCount++;
            }
        }
        
    }

    public void CreateMesh() {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
