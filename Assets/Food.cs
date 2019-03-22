using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    [SerializeField] GameObject foodObject;
    [SerializeField] Transform parent;
    Positions foodPosition;
    // Start is called before the first frame update

    public void SpawnFood() {

        List<Positions> nonOccupiedPos = new List<Positions>(); //creating array to hold the non occupied positions

        for (int n = 0; n < ProceduralMesh.gridSize * ProceduralMesh.gridSize;n++) {

            if (!ProceduralMesh.positions[n].IsOcuppied) {
                nonOccupiedPos.Add(ProceduralMesh.positions[n]);
            }
        }
        int randPos = Random.Range(0, nonOccupiedPos.Count);
        print(randPos);
        print(nonOccupiedPos.Count);
        print(ProceduralMesh.positions[randPos].gameObject.name);
        GameObject snakePart = Instantiate(foodObject, new Vector3(ProceduralMesh.positions[randPos].gameObject.transform.position.x,
                                                                   ProceduralMesh.positions[randPos].gameObject.transform.position.y,
                                                                   ProceduralMesh.positions[randPos].gameObject.transform.position.z), Quaternion.identity, parent);
        ProceduralMesh.positions[randPos].HasFood = true;
    }

    Positions FoodPosition {
        get { return foodPosition; }
        set { foodPosition = value; }
    }
}
