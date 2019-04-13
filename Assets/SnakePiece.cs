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
        if (Snake.Instance.snakeSize == 0) {
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
}
