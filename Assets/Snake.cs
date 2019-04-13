using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    public static Snake Instance { get; private set; }

    // Start is called before the first frame update
    [SerializeField] GameObject snakePieceObejct;
    [SerializeField] Transform parent;
    List<SnakePiece> snakeBody;

    private bool isDead = false;
    public int snakeSize = 0;

    //variable created just to not use "ProceduralMesh.gridSize * ProceduralMesh.gridSize" everytime

    void Awake() {
        Instance = this;
    }

    void Start() {
        snakeBody = new List<SnakePiece>();
    }

    public void SpawnSnakePiece() {
        int x, y;

        if (snakeSize == 0) {
            //spawn a game object in an x and y value that is not located in an edge of the Board, so that there are no colisions
            //with the walls when the game starts
            GameObject snakePart = Instantiate(snakePieceObejct, new Vector3(x = Random.Range(1, ProceduralMesh.gridSize-1),
                                                                             y = Random.Range(1, ProceduralMesh.gridSize-1),
                                                                             0), Quaternion.identity, parent);
            print(x + " " + y);
            snakeBody.Add(snakePart.GetComponent<SnakePiece>());
            snakeBody[snakeSize].Position = ProceduralMesh.positions[x + ProceduralMesh.gridSize * y];
            snakeBody[snakeSize].Position.IsOcuppied = true;
            snakeSize++;

        }
        //spawn a piece of the snake in the last position that the last piece is located
        else if(snakeSize < ProceduralMesh.gridSize * ProceduralMesh.gridSize) {

            GameObject snakePart = Instantiate(snakePieceObejct, new Vector3(snakeBody[snakeSize-1].transform.position.x, 
                                                                             snakeBody[snakeSize-1].transform.position.y, 
                                                                             snakeBody[snakeSize-1].transform.position.z), Quaternion.identity, parent);
            snakeBody.Add(snakePart.GetComponent<SnakePiece>());
            snakeBody[snakeSize].Position = snakeBody[snakeSize-1].Position;
            snakeBody[snakeSize].Position.IsOcuppied = true;
            snakeBody[snakeSize].Direction = snakeBody[snakeSize-1].Direction;
            snakeSize++;
        }
    }

    //This method is the reponsible for moving the snake in certain the direction the player chooses (up, rigth, down and left).
    //To this project I decide to do the movement based on the LAST POSITION TO THE FIRST, so that is easier to spawn pieces in 
    //the last position of the snake. The code is not optimal, but it works.
    public bool Move(Positions[] positions, Direction dir) {

        int sSize;

        if (!CheckDead(dir)) {
            sSize = snakeSize;
            //checks if the snake is going to eat food (if yes, spawns a new SnakePiece imitating the last snakeBody index parameters)
            //increments snakeSize if the snake ate food
            if (CheckEat(dir)) {
                SpawnSnakePiece();
            }

            for (int lastPos = sSize - 1; lastPos >= 0; lastPos--) {
                //if the snake has only on piece, set the last pos (Occupied) to false, since no piece is going to be there.
                if (lastPos == 0 && snakeBody.Count == 1) {

                    if (snakeBody[lastPos].Position.Neighbours.east >= 0 && dir == Direction.East) {

                        snakeBody[lastPos].Direction = dir;
                        snakeBody[lastPos].transform.position += new Vector3(1, 0, 0); // consider using Vector3.up/left/rigth/down
                        snakeBody[lastPos].Position.IsOcuppied = false;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.east];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.south >= 0 && dir == Direction.South) {

                        snakeBody[lastPos].Direction = dir;
                        snakeBody[lastPos].transform.position += new Vector3(0, -1, 0);
                        snakeBody[lastPos].Position.IsOcuppied = false;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.south];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.west >= 0 && dir == Direction.West) {

                        snakeBody[lastPos].Direction = dir;
                        snakeBody[lastPos].transform.position += new Vector3(-1, 0, 0);
                        snakeBody[lastPos].Position.IsOcuppied = false;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.west];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.north >= 0 && dir == Direction.North) {

                        snakeBody[lastPos].Direction = dir;
                        snakeBody[lastPos].transform.position += new Vector3(0, 1, 0);
                        snakeBody[lastPos].Position.IsOcuppied = false;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.north];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }
                }
                //if is the first positios (head of the Snake) and the snake has more than one piece, then set the new position (Occupied) to true,
                //and let the old one the way it is (true), so that the next piece of the snake can occupie that position
                else if (lastPos == 0 && snakeBody.Count > 1) {

                    if (snakeBody[lastPos].Position.Neighbours.east >= 0 && dir == Direction.East) {

                        snakeBody[lastPos].Direction = dir;
                        snakeBody[lastPos].transform.position += new Vector3(1, 0, 0); // consider using Vector3.up/left/rigth/down
                        snakeBody[lastPos].Position.IsOcuppied = true;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.east];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.south >= 0 && dir == Direction.South) {

                        snakeBody[lastPos].Direction = dir;
                        snakeBody[lastPos].transform.position += new Vector3(0, -1, 0);
                        snakeBody[lastPos].Position.IsOcuppied = true;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.south];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.west >= 0 && dir == Direction.West) {

                        snakeBody[lastPos].Direction = dir;
                        snakeBody[lastPos].transform.position += new Vector3(-1, 0, 0);
                        snakeBody[lastPos].Position.IsOcuppied = true;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.west];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.north >= 0 && dir == Direction.North) {

                        snakeBody[lastPos].Direction = dir;
                        snakeBody[lastPos].transform.position += new Vector3(0, 1, 0);
                        snakeBody[lastPos].Position.IsOcuppied = true;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.north];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }
                }
                //if is not the last position of the snake (the botton of the list), then set the new position (Occupied) to true,
                //and let the old one the way it is (true), so that the next piece of the snake can occupie that position
                else if (lastPos < snakeSize - 1) {

                    if (snakeBody[lastPos].Position.Neighbours.east >= 0 && snakeBody[lastPos - 1].Direction == Direction.East) {

                        snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                        snakeBody[lastPos].transform.position += new Vector3(1, 0, 0); // consider using Vector3.up/left/rigth/down
                        snakeBody[lastPos].Position.IsOcuppied = true;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.east];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.south >= 0 && snakeBody[lastPos - 1].Direction == Direction.South) {

                        snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                        snakeBody[lastPos].transform.position += new Vector3(0, -1, 0);
                        snakeBody[lastPos].Position.IsOcuppied = true;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.south];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.west >= 0 && snakeBody[lastPos - 1].Direction == Direction.West) {

                        snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                        snakeBody[lastPos].transform.position += new Vector3(-1, 0, 0);
                        snakeBody[lastPos].Position.IsOcuppied = true;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.west];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.north >= 0 && snakeBody[lastPos - 1].Direction == Direction.North) {

                        snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                        snakeBody[lastPos].transform.position += new Vector3(0, 1, 0);
                        snakeBody[lastPos].Position.IsOcuppied = true;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.north];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }
                }
                //if the snake is at its last position set the old pos (Occupied) to false, since there is no pieces occuping that spot anymore
                else if (lastPos == snakeSize - 1) {

                    if (snakeBody[lastPos].Position.Neighbours.east >= 0 && snakeBody[lastPos - 1].Direction == Direction.East) {

                        snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                        snakeBody[lastPos].transform.position += new Vector3(1, 0, 0); // consider using Vector3.up/left/rigth/down
                        snakeBody[lastPos].Position.IsOcuppied = false;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.east];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.south >= 0 && snakeBody[lastPos - 1].Direction == Direction.South) {

                        snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                        snakeBody[lastPos].transform.position += new Vector3(0, -1, 0);
                        snakeBody[lastPos].Position.IsOcuppied = false;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.south];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.west >= 0 && snakeBody[lastPos - 1].Direction == Direction.West) {

                        snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                        snakeBody[lastPos].transform.position += new Vector3(-1, 0, 0);
                        snakeBody[lastPos].Position.IsOcuppied = false;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.west];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }

                    else if (snakeBody[lastPos].Position.Neighbours.north >= 0 && snakeBody[lastPos - 1].Direction == Direction.North) {

                        snakeBody[lastPos].Direction = snakeBody[lastPos - 1].Direction;
                        snakeBody[lastPos].transform.position += new Vector3(0, 1, 0);
                        snakeBody[lastPos].Position.IsOcuppied = false;
                        snakeBody[lastPos].Position = positions[snakeBody[lastPos].Position.Neighbours.north];
                        snakeBody[lastPos].Position.IsOcuppied = true;
                    }
                }
            }
            //return false (player not dead and moved)
            print(isDead);
            return isDead;

        }

        else {
            print(isDead);
            //player died
            return isDead;
        }
    }
    //check if the snake as eated something
    public bool CheckEat(Direction dir) {

        if (dir == Direction.North && snakeBody[0].Position.Neighbours.north >= 0 && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.north].HasFood) {

            return true;
        }
        else if(dir == Direction.East && snakeBody[0].Position.Neighbours.east >= 0 && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.east].HasFood) {

            return true;
        }
        else if (dir == Direction.South && snakeBody[0].Position.Neighbours.south >= 0 && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.south].HasFood) {

            return true;
        }
        else if (dir == Direction.West && snakeBody[0].Position.Neighbours.west >= 0 && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.west].HasFood) {

            return true;
        }

        else {
            return false;
        }

    }

    bool CheckDead(Direction dir) {
        //problem with the tests getting out of bounds if the player if close to an edge
        if (dir == Direction.East && dir == Direction.East && snakeBody[0].Position.Neighbours.east == -1) {
            isDead = true;
            return isDead;
        }
        else if (dir == Direction.East && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.east].IsOcuppied) {
            isDead = true;
            return isDead;
        }

        else if (dir == Direction.South && dir == Direction.South && snakeBody[0].Position.Neighbours.south == -1) {
            isDead = true;
            return isDead;
        }
        else if (dir == Direction.South && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.south].IsOcuppied) {
            isDead = true;
            return isDead;
        }

        else if (dir == Direction.West && dir == Direction.West &&snakeBody[0].Position.Neighbours.west == -1) {
            isDead = true;
            return isDead;
        }
        else if (dir == Direction.West && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.west].IsOcuppied) {
            isDead = true;
            return isDead;
        }

        else if (dir == Direction.North && dir == Direction.North && snakeBody[0].Position.Neighbours.north == -1) {
            isDead = true;
            return isDead;
        }
        else if (dir == Direction.North && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.north].IsOcuppied) {
            isDead = true;
            return isDead;
        }

        else {
            isDead = false;
            return false;
        }
        
    }

}

