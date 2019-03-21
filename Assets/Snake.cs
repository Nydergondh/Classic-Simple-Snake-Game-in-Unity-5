using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    // Start is called before the first frame update
    [SerializeField] GameObject snakePieceObejct;
    [SerializeField] Transform parent;
    List<SnakePiece> snakeBody;

    Direction direction;
    public static int snakeSize = 0;

    void Start() {
        snakeBody = new List<SnakePiece>();
    }

    public Direction Direction{
        get{ return  direction;}
        set { direction = value;}
    }

    public void SpawnSnakePiece() {
        int x, y;

        if (snakeSize == 0) {
            GameObject snakePart = Instantiate(snakePieceObejct, new Vector3(x = Random.Range(0, ProceduralMesh.gridSize), 
                                                                             y = Random.Range(0, ProceduralMesh.gridSize),
                                                                             0                                          ), Quaternion.identity, parent);
            snakeBody.Add(snakePart.GetComponent<SnakePiece>());
            snakeBody[snakeSize].Position = ProceduralMesh.positions[x + ProceduralMesh.gridSize * y];
            print("start pos = "+ snakeBody[snakeSize].Position.QuadPosition);
            RandomDirection();
            snakeSize++;
        }

    }

    public void RandomDirection() {
        int n = Random.Range(0,4);

        if (n == 0) {
            direction = Direction.East;
        }

        else if (n == 1) {
            direction = Direction.South;
        }

        else if (n == 2) {
            direction = Direction.West;
        }

        else {  
            direction = Direction.North;
        }
    }

    public void Move(Positions[] positions) {

        foreach (SnakePiece snakePiece in snakeBody) {

            if (snakePiece.Position.neighbours.east >= 0 && direction == Direction.East) {
                print(snakePiece.Position.neighbours.east);
                snakePiece.transform.position += new Vector3(1, 0, 0);
                snakePiece.Position.IsOcuppied = false;
                print("Old pos = " + snakePiece.Position.QuadPosition);
                snakePiece.Position = positions[snakePiece.Position.neighbours.east];
                print("New pos = " + snakePiece.Position.QuadPosition);
                snakePiece.Position.IsOcuppied = true;
            }

            else if (snakePiece.Position.neighbours.south >= 0 && direction == Direction.South) {                
                snakePiece.transform.position += new Vector3(0, -1, 0);
                snakePiece.Position.IsOcuppied = false;
                print("Old pos = " + snakePiece.Position.QuadPosition);
                snakePiece.Position = positions[snakePiece.Position.neighbours.south];
                print("New pos = " + snakePiece.Position.QuadPosition);
                snakePiece.Position.IsOcuppied = true;
            }

            else if (snakePiece.Position.neighbours.west >= 0 && direction == Direction.West) {
                print(snakePiece.Position.neighbours.west);
                snakePiece.transform.position += new Vector3(-1, 0, 0);
                snakePiece.Position.IsOcuppied = false;
                print("Old pos = " + snakePiece.Position.QuadPosition);
                snakePiece.Position = positions[snakePiece.Position.neighbours.west];
                print("New pos = " + snakePiece.Position.QuadPosition);
                snakePiece.Position.IsOcuppied = true;
            }            

            else if(snakePiece.Position.neighbours.north >= 0 && direction == Direction.North) {
                print(snakePiece.Position.neighbours.north);
                snakePiece.transform.position += new Vector3(0, 1, 0);
                snakePiece.Position.IsOcuppied = false;
                print("Old pos = " + snakePiece.Position.QuadPosition);
                snakePiece.Position = positions[snakePiece.Position.neighbours.north];
                print("New pos = " + snakePiece.Position.QuadPosition);
                snakePiece.Position.IsOcuppied = true;
            }
        }

    }
}
