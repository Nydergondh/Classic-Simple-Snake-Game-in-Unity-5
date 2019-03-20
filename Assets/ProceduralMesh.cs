using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter), (typeof(MeshRenderer)))] 
public class ProceduralMesh : MonoBehaviour
{

    Mesh mesh;
    public static Positions[] positions;
    public static int gridSize;

    [SerializeField] GameObject gameObject;
    [SerializeField] Transform parent;
    Snake snake;

    [SerializeField] float cellSize;     
    
    // Start is called before the first frame update
    void Start() {
        gridSize = 3;
        mesh = GetComponent<MeshFilter>().mesh;
        MakeMeshData();
        snake = GetComponentInChildren<Snake>();
        snake.SpawnSnakePiece();
    }

    // Update is called once per frame
    void Update() { 

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            snake.Direction = Direction.East;
            snake.Move(positions);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            snake.Direction = Direction.South;
            snake.Move(positions);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            snake.Direction = Direction.West;
            snake.Move(positions);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            snake.Direction = Direction.North;
            snake.Move(positions);
        }        
    }

    public void MakeMeshData() {

        positions = new Positions[gridSize * gridSize]; // adjusting how many positions there are in the grid
        
        for (int l = 0, posCount = 0; l < gridSize ;l++) {
            for (int c = 0; c < gridSize; c++) {

                GameObject pos = Instantiate(gameObject, new Vector3(c * cellSize, l * cellSize, 0), Quaternion.identity, parent); //create new position at the quad. 
                positions[posCount] = pos.GetComponent<Positions>();

                positions[posCount].QuadPosition = posCount+1; //create the grid while adding a value to the postion variable.
                positions[posCount].SetNeighbours(gridSize);
                posCount++;

            }
        }    
    }

}
