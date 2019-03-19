using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Positions : MonoBehaviour {
    private int quadPosition;
    private bool isOccupied; // check if the snake is at this position rigth now
    int[] neighbours = new int[4];

    // Start is called before the first frame update
    void Awake() {
        isOccupied = false;
    }

    // Update is called once per frame

    public int QuadPosition {
        get { return quadPosition; }
        set { quadPosition = value; }
    }

    public bool IsOcuppied {
        get { return isOccupied; }
        set { isOccupied = value; }
    }

    public void SetNeighbours(int gridSize) {
        if (quadPosition % gridSize == 0) { //east
            neighbours[0] = -1; //in case the east dosent exist
        }
        else {
            neighbours[0] = quadPosition + 1; //in case East exists
        }

        if (quadPosition - gridSize < 0) { //south
            neighbours[1] = -1; //in case the south dosent exist
        }
        else {
            neighbours[1] = quadPosition - gridSize; //in case South exists
        }

        if (quadPosition - 1 % gridSize == 0) {//north
            neighbours[2] = -1; //in case the north dosent exist
        }
        else {
            neighbours[2] = quadPosition + gridSize;
        }

        if (quadPosition + gridSize > gridSize * gridSize) {//west
            neighbours[3] = -1; //in case the west dosent exist
        }
        else {
            neighbours[3] = -1; //in case west exists
        }
    }

}
