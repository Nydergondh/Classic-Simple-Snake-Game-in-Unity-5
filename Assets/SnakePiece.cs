using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePiece : MonoBehaviour
{
    Positions position;
    Direction direction;

    bool isHead = false;


    // Start is called before the first frame update
    void Start()
    {
        if (Snake.snakeSize == 0) {
            isHead = true;
        }
    }
    
    public Direction Direction {
        get { return direction; }
        set { direction = value; }
    }

    public Positions Position {
        get { return position; }
        set { position = value; }
    }
    public bool IsHead {
        get { return isHead; }
        set { isHead = value; }
    }

    public void RandomDirection() {
        int n = Random.Range(0, 4);

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
}
