using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            snakeSize++;
            print("Snake Pieces"+ snakeBody.Count);
        }


    }

    public void Move(Positions[] positions, Direction dir) {

        //checks if the snake is going to eat food (if yes, spawns a new SnakePiece imitating the last snakeBody index parameters)
        //increments snakeSize if the snake ate food
        if (!CheckDead(dir)) {


            if (CheckEat(dir)) {
                SpawnSnakePiece();
            }

            for (int lastPos = snakeSize - 1; lastPos >= 0; lastPos--) {

                if (lastPos == 0) {

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

                else if (!CheckEat(dir) || (lastPos != snakeSize - 1 && CheckEat(dir))) {
                    //checking if a piece has not just spawned. If it has spawned, then nothing will be done, otherwise movement will occur.
                    //since spawned pieces mimic element of the last position of the List of the snake before moving, there is no need to move it
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
        }

        else if (snakeSize == ProceduralMesh.gridSize * ProceduralMesh.gridSize) {
            WinGame();
        }
        else {
            Dying();
        }

    }
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

    IEnumerator WinGame() {
        print("You Won!!!");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    IEnumerator Dying() {
        print("You Died.");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }

    bool CheckDead(Direction dir) {
        //problem with the tests getting out of bounds if the player si close to an edge
        if (dir == Direction.East && dir == Direction.East && snakeBody[0].Position.Neighbours.east == -1) {
            return true;
        }
        else if (dir == Direction.East && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.east].IsOcuppied) {
            return true;
        }

        else if (dir == Direction.South && dir == Direction.South && snakeBody[0].Position.Neighbours.south == -1) {
            return true;
        }
        else if (dir == Direction.South && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.south].IsOcuppied) {
            return true;
        }

        else if (dir == Direction.West && dir == Direction.West &&snakeBody[0].Position.Neighbours.west == -1) {
             return true;
        }
        else if (dir == Direction.West && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.west].IsOcuppied) {
            return true;
        }

        else if (dir == Direction.North && dir == Direction.North && snakeBody[0].Position.Neighbours.north == -1) {
             return true;
        }
        else if (dir == Direction.North && ProceduralMesh.positions[snakeBody[0].Position.Neighbours.north].IsOcuppied) {
            return true;
        }

        else {
            return false;
        }
        
    }

}
