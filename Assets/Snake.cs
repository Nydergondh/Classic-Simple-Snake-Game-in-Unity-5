using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    // Start is called before the first frame update
    [SerializeField] GameObject snakePieceObejct;
    [SerializeField] Transform parent;
    List<SnakePiece> snakeBody;

    public static int snakeSize = 0;
    
    void Start() {
        snakeBody = new List<SnakePiece>();
    }

    public void SpawnSnakePiece() {
        int x, y;
        
        if (snakeSize == 0) {
            GameObject snakePart = Instantiate(snakePieceObejct, new Vector3(x = Random.Range(0, ProceduralMesh.gridSize),
                                                                             y = Random.Range(0, ProceduralMesh.gridSize),
                                                                             0), Quaternion.identity, parent);

            snakeBody.Add(snakePart.GetComponent<SnakePiece>());
            snakeBody[snakeSize].Position = ProceduralMesh.positions[x + ProceduralMesh.gridSize * y];
            snakeBody[snakeSize].RandomDirection();
            snakeSize++;
        }

    }

    public void Move(Positions[] positions, Direction dir) {

        foreach (SnakePiece snakePiece in snakeBody) {

            if (snakePiece.Position.neighbours.east >= 0 && dir == Direction.East) {
                snakePiece.Direction = dir;
                snakePiece.transform.position += new Vector3(1, 0, 0);
                snakePiece.Position.IsOcuppied = false;
                snakePiece.Position = positions[snakePiece.Position.neighbours.east];
                snakePiece.Position.IsOcuppied = true;
            }

            else if (snakePiece.Position.neighbours.south >= 0 && dir == Direction.South) {
                snakePiece.Direction = dir;
                snakePiece.transform.position += new Vector3(0, -1, 0);
                snakePiece.Position.IsOcuppied = false;
                snakePiece.Position = positions[snakePiece.Position.neighbours.south];
                snakePiece.Position.IsOcuppied = true;
            }

            else if (snakePiece.Position.neighbours.west >= 0 && dir == Direction.West) {
                snakePiece.Direction = dir;
                snakePiece.transform.position += new Vector3(-1, 0, 0);
                snakePiece.Position.IsOcuppied = false;
                snakePiece.Position = positions[snakePiece.Position.neighbours.west];
                snakePiece.Position.IsOcuppied = true;
            }            

            else if(snakePiece.Position.neighbours.north >= 0 && dir == Direction.North) { 
                snakePiece.Direction = dir;
                snakePiece.transform.position += new Vector3(0, 1, 0);
                snakePiece.Position.IsOcuppied = false;
                snakePiece.Position = positions[snakePiece.Position.neighbours.north];
                snakePiece.Position.IsOcuppied = true;
            }
        }
    }
}
