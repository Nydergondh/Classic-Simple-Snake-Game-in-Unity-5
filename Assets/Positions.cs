using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positions : MonoBehaviour {
    private int quadPosition;
    private bool isOccupied = false;
    private bool hasFood = false;
    private neighbours neighbour;

    // Update is called once per frame

    public int QuadPosition {
        get { return quadPosition; }
        set { quadPosition = value; }
    }

    public bool IsOcuppied {
        get { return isOccupied; }
        set { isOccupied = value; }
    }

    public bool HasFood {
        get { return hasFood; }
        set { hasFood = value; }
    }

    public neighbours Neighbours {
        get { return neighbour; }
    }

    public void SetNeighbours(int gridSize) {
        if ((quadPosition + 1) % gridSize == 0) { //east
           neighbour.east = -1; //in case the east dosent exist
        }
        else {
            neighbour.east = quadPosition + 1; //in case East exists
        }

        if (quadPosition + 1 - gridSize < 0) { //south
            neighbour.south = -1; //in case the south dosent exist
        }
        else {
            neighbour.south  = quadPosition - gridSize; //in case South exists
        }

        if (quadPosition + 1 + gridSize > gridSize * gridSize) {//north
            neighbour.north = -1; //in case the north dosent exist
        }
        else {
            neighbour.north = quadPosition + gridSize; //in case north exists
        }

        if (quadPosition % gridSize == 0) {//west
            neighbour.west = - 1; //in case the west dosent exist
        }
        else {
            neighbour.west = quadPosition -1; //in case west exists
        }
    }

}
