using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(MeshFilter), (typeof(MeshRenderer)))]
public class ProceduralMesh : MonoBehaviour {

    Mesh mesh;
    
    public static Positions[] positions;
    public static int gridSize;

    [SerializeField] GameObject gameObject;
    [SerializeField] Transform parent;

    Snake snake;
    Food food;
    Direction dirMove;

    [SerializeField] float cellSize;

    private bool isMoving;
    private bool died;
    private bool hasStarted = false;

    // Start is called before the first frame update
    void Start() {
        
        gridSize = 5;
        died = false;
        isMoving = false;
        dirMove = RandomDirection();

        //generate the board
        mesh = GetComponent<MeshFilter>().mesh;
        MakeMeshData();

        //spawns the first snake piece
        snake = GetComponentInChildren<Snake>();
        snake.SpawnSnakePiece();

        //spawns the first food
        food = GetComponentInChildren<Food>();
        food.SpawnFood();

        //waits for player input to move

    }

    // Update is called once per frame
    void Update() {


        if (hasStarted) {

            if (Input.anyKeyDown) {
                ChooseDirection();
            }

            TryToMove();
        }

        else {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
                StartGame();
            }
        }

    }

    private void TryToMove() {

        if (!isMoving) {

            if (!died) {
                isMoving = true;
                StartCoroutine(MoveSnake());
            }

            if (died && snake.snakeSize == positions.Length) {
                StartCoroutine(WinGame());
            }

            else if (died) {
                StartCoroutine(Dying());
            }

        }

    }

    public void MakeMeshData() {

        positions = new Positions[gridSize * gridSize]; // adjusting how many positions there are in the grid

        for (int l = 0, posCount = 0; l < gridSize; l++) {

            for (int c = 0; c < gridSize; c++) {

                GameObject pos = Instantiate(gameObject, new Vector3(c * cellSize, l * cellSize, 0), Quaternion.identity, parent); //create new position at the quad. 
                positions[posCount] = pos.GetComponent<Positions>();

                positions[posCount].QuadPosition = posCount; //create the grid while adding a value to the postion variable.
                positions[posCount].SetNeighbours(gridSize);
                posCount++;

            }

        }

    }

    //move the snake in an interval of 0.5 sec
    IEnumerator MoveSnake() {

        died = snake.Move(positions, dirMove);
        yield return new WaitForSeconds(0.5f);
        isMoving = false;

    }

    IEnumerator WinGame() {

        yield return new WaitForSeconds(5f);
        print("You Won!!!");
        SceneManager.LoadScene(0);

    }

    IEnumerator Dying() {

        yield return new WaitForSeconds(5f);
        print("You Died.");
        SceneManager.LoadScene(0);

    }

    public Direction RandomDirection() {

        int n = Random.Range(0, 4);

        if (n == 0) {
            return Direction.East;
        }

        else if (n == 1) {
            return Direction.South;
        }

        else if (n == 2) {
            return Direction.West;
        }

        else {
            return Direction.North;
        }
    }

    private void StartGame() {
        
        ChooseDirection();
        hasStarted = true;
                
    }

    private void ChooseDirection() {

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            dirMove = Direction.East;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            dirMove = Direction.South;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            dirMove = Direction.West;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow)) {
            dirMove = Direction.North;
        }  

    }
    
}
