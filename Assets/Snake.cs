using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    // Start is called before the first frame update
    [SerializeField] GameObject snakePieceObejct;
    [SerializeField] Transform parent;
    List<SnakePiece> snakeBody;

    public static int snakeSize = 0;
    int gridSize = ProceduralMesh.gridSize * ProceduralMesh.gridSize; //variable created just to no use "ProceduralMesh.gridSize * ProceduralMesh.gridSize" everytime

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
        else if(snakeSize < gridSize) {

        }

    }

    public void Move(Positions[] positions, Direction dir) {

        snakeBody[0].Direction = dir;
        for (int lastPos = snakeSize-1; lastPos >= 0; lastPos--) {

            if (snakeBody[lastPos].Position.neighbours.east >= 0 && snakeBody[lastPos].Direction == Direction.East) {
                if (lastPos == 0) {
                    snakeBody[lastPos].Direction = dir;
                }
                else {
                    snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                }
                snakeBody[lastPos].transform.position += new Vector3(1, 0, 0); // consider using Vector3.up/left/rigth/down
                snakeBody[lastPos].Position.IsOcuppied = false;
                snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.east];
                snakeBody[lastPos].Position.IsOcuppied = true;
            }

            else if (snakeBody[lastPos].Position.neighbours.south >= 0 && dir == Direction.South) {

                if (lastPos == 0) {
                    snakeBody[lastPos].Direction = dir;
                }
                else {
                    snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                }
                snakeBody[lastPos].Direction = dir;
                snakeBody[lastPos].transform.position += new Vector3(0, -1, 0);
                snakeBody[lastPos].Position.IsOcuppied = false;
                snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.south];
                snakeBody[lastPos].Position.IsOcuppied = true;
            }

            else if (snakeBody[lastPos].Position.neighbours.west >= 0 && dir == Direction.West) {

                if (lastPos == 0) {
                    snakeBody[lastPos].Direction = dir;
                }
                else {
                    snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                }
                snakeBody[lastPos].Direction = dir;
                snakeBody[lastPos].transform.position += new Vector3(-1, 0, 0);
                snakeBody[lastPos].Position.IsOcuppied = false;
                snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.west];
                snakeBody[lastPos].Position.IsOcuppied = true;
            }

            else if (snakeBody[lastPos].Position.neighbours.north >= 0 && dir == Direction.North) {

                if (lastPos == 0) {
                    snakeBody[lastPos].Direction = dir;
                }
                else {
                    snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                }
                snakeBody[lastPos].Direction = dir;
                snakeBody[lastPos].transform.position += new Vector3(0, 1, 0);
                snakeBody[lastPos].Position.IsOcuppied = false;
                snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.north];
                snakeBody[lastPos].Position.IsOcuppied = true;
            }
        }
    /*
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
    */
    }
}
