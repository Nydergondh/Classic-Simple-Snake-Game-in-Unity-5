using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    // Start is called before the first frame update
    [SerializeField] GameObject snakePieceObejct;
    [SerializeField] Transform parent;
    List<SnakePiece> snakeBody;

    public static int snakeSize = 0;

    //variable created just to not use "ProceduralMesh.gridSize * ProceduralMesh.gridSize" everytime

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
            snakeBody[snakeSize].Position.IsOcuppied = true;
            snakeBody[snakeSize].RandomDirection();
            snakeSize++;
        }

        else if(snakeSize < ProceduralMesh.gridSize * ProceduralMesh.gridSize) {

            GameObject snakePart = Instantiate(snakePieceObejct, new Vector3(snakeBody[snakeSize-1].transform.position.x, 
                                                                             snakeBody[snakeSize-1].transform.position.y, 
                                                                             snakeBody[snakeSize-1].transform.position.z), Quaternion.identity, parent);
            snakeBody.Add(snakePart.GetComponent<SnakePiece>());
            snakeBody[snakeSize].Position = snakeBody[snakeSize-1].Position;
            snakeBody[snakeSize].Position.IsOcuppied = true;
            snakeBody[snakeSize].Direction = snakeBody[snakeSize-1].Direction;
        }

    }

    public void Move(Positions[] positions, Direction dir) {

        //checks if the snake is going to eat food (if yes, spawns a new SnakePiece imitating the last snakeBody index parameters)
        //increments snakeSize if the snake ate food
        if (CheckEat(dir)) {

            SpawnSnakePiece();
            snakeSize++;
        }

        for (int lastPos = snakeSize-1; lastPos >= 0; lastPos--) {

            if (lastPos == 0) {
                print(lastPos);
                if (snakeBody[lastPos].Position.neighbours.east >= 0 && dir == Direction.East) {
                    
                    snakeBody[lastPos].Direction = dir;
                    snakeBody[lastPos].transform.position += new Vector3(1, 0, 0); // consider using Vector3.up/left/rigth/down
                    snakeBody[lastPos].Position.IsOcuppied = false;
                    snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.east];
                    snakeBody[lastPos].Position.IsOcuppied = true;
                }

                else if (snakeBody[lastPos].Position.neighbours.south >= 0 && dir == Direction.South) {

                    snakeBody[lastPos].Direction = dir;
                    snakeBody[lastPos].transform.position += new Vector3(0, -1, 0);
                    snakeBody[lastPos].Position.IsOcuppied = false;
                    snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.south];
                    snakeBody[lastPos].Position.IsOcuppied = true;
                }

                else if (snakeBody[lastPos].Position.neighbours.west >= 0 && dir == Direction.West) {

                    snakeBody[lastPos].Direction = dir;
                    snakeBody[lastPos].transform.position += new Vector3(-1, 0, 0);
                    snakeBody[lastPos].Position.IsOcuppied = false;
                    snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.west];
                    snakeBody[lastPos].Position.IsOcuppied = true;
                }

                else if (snakeBody[lastPos].Position.neighbours.north >= 0 && dir == Direction.North) {

                    snakeBody[lastPos].Direction = dir;
                    snakeBody[lastPos].transform.position += new Vector3(0, 1, 0);
                    snakeBody[lastPos].Position.IsOcuppied = false;
                    snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.north];
                    snakeBody[lastPos].Position.IsOcuppied = true;
                }
            }

            else {

                if (snakeBody[lastPos].Position.neighbours.east >= 0 && snakeBody[lastPos - 1].Direction == Direction.East) {

                    snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                    snakeBody[lastPos].transform.position += new Vector3(1, 0, 0); // consider using Vector3.up/left/rigth/down
                    snakeBody[lastPos].Position.IsOcuppied = false;
                    snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.east];
                    snakeBody[lastPos].Position.IsOcuppied = true;
                }

                else if (snakeBody[lastPos].Position.neighbours.south >= 0 && snakeBody[lastPos - 1].Direction == Direction.South) {

                    snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                    snakeBody[lastPos].transform.position += new Vector3(0, -1, 0);
                    snakeBody[lastPos].Position.IsOcuppied = false;
                    snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.south];
                    snakeBody[lastPos].Position.IsOcuppied = true;
                }

                else if (snakeBody[lastPos].Position.neighbours.west >= 0 && snakeBody[lastPos - 1].Direction == Direction.West) {

                    snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                    snakeBody[lastPos].transform.position += new Vector3(-1, 0, 0);
                    snakeBody[lastPos].Position.IsOcuppied = false;
                    snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.west];
                    snakeBody[lastPos].Position.IsOcuppied = true;
                }

                else if (snakeBody[lastPos].Position.neighbours.north >= 0 && snakeBody[lastPos - 1].Direction == Direction.North) {

                    snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                    snakeBody[lastPos].transform.position += new Vector3(0, 1, 0);
                    snakeBody[lastPos].Position.IsOcuppied = false;
                    snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.neighbours.north];
                    snakeBody[lastPos].Position.IsOcuppied = true;
                }
            }
            
        }
    
    }

    public bool CheckEat(Direction dir) {

        if (dir == Direction.North && snakeBody[0].Position.neighbours.north >= 0 && ProceduralMesh.positions[snakeBody[0].Position.neighbours.north].HasFood) {

            return true;
        }
        else if(dir == Direction.East && snakeBody[0].Position.neighbours.east >= 0 && ProceduralMesh.positions[snakeBody[0].Position.neighbours.east].HasFood) {

            return true;
        }
        else if (dir == Direction.South && snakeBody[0].Position.neighbours.south >= 0 && ProceduralMesh.positions[snakeBody[0].Position.neighbours.south].HasFood) {

            return true;
        }
        else if (dir == Direction.West && snakeBody[0].Position.neighbours.west >= 0 && ProceduralMesh.positions[snakeBody[0].Position.neighbours.west].HasFood) {

            return true;
        }

        else {
            return false;
        }
    }
}
