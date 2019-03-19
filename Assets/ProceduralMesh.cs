using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), (typeof(MeshRenderer)))] 
public class ProceduralMesh : MonoBehaviour
{

    Mesh mesh;
    Positions[] positions;
    [SerializeField] GameObject gameObejct;
    [SerializeField] Transform parent;

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
        MakeMeshData();
        CreateMesh();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void MakeMeshData() {

        vertices = new Vector3[gridSize * gridSize * 4]; // adjusting the quantity of vertices in the grid
        triangles = new int[gridSize * gridSize * 6]; // adjusting the quantity of triangles in the grid
        positions = new Positions[gridSize * gridSize]; // adjusting how many positions there are in the grid
        vertexOffset = cellSize * 0.5f;

        int v = 0; //vertices
        int t = 0; // triangles

        //creates one quad at every loop in the second for
        for (int l = 0, posCount = 0; l < gridSize ;l++) {
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

                v += 4; //adjust the count of were the vertices and triangles are
                t += 6;

                GameObject pos = Instantiate(gameObejct, new Vector3(c * cellSize, l * cellSize, 0), Quaternion.identity, parent); //create new position at the quad. 
                positions[posCount] = pos.GetComponent<Positions>();

                positions[posCount].QuadPosition = posCount+1; //create the grid while adding a value to the postion variable.
                positions[posCount].SetNeighbours(gridSize);
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
