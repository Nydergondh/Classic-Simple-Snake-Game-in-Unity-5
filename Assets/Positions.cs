using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positions : MonoBehaviour
{
    private int quadPosition;
    private bool isOccupied; // check if the snake is at this position rigth now
    Positions[] neighbours = new Positions[4];

    // Start is called before the first frame update
    void Awake()
    {
        isOccupied = false;
    }

    // Update is called once per frame

    public int QuadPosition {
        get { return quadPosition; }
        set { quadPosition = value; }
    }

    public bool IsOcuppied {
        get{ return isOccupied; }
        set{ isOccupied = value; }
    }

    public void SetNeighbours(int gridSize) {
        //TODO make code that Set the Position Neighbours.
    }

    
}
