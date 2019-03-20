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

    public Positions Position {
        get { return position; }
        set { position = value; }
    }

}
