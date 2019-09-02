using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherNature : MonoBehaviour {
    [SerializeField]
    GameObject field;
    [SerializeField]
    int secondsToPlaceWater = 3;
    [SerializeField]
    int secondsToPlaceFood = 3;
    [SerializeField]
    GameObject waterPrefab;
    [SerializeField]
    GameObject foodPrefab;

    void Start() {
        InvokeRepeating("placeWater", 0.0f, this.secondsToPlaceWater);
        InvokeRepeating("placeFood", 0.0f, this.secondsToPlaceFood);
    }

    private Vector3 randomPointInSquare(Vector3 center, Vector3 size) {
       float groundFloor = 0.0f;
       return center + new Vector3((Random.value - 0.5f) * size.x, groundFloor, (Random.value - 0.5f) * size.z);
    }

    private void placeGameObject(GameObject gameObject, Vector3 location) {
        if(gameObject != null) {
            Instantiate(gameObject, location, Quaternion.identity);
            Debug.Log("placed " + gameObject.name + " at " + location);
        }
    }

    private void placeWater() {
        this.placeGameObject(this.waterPrefab, this.randomPointInSquare(this.field.transform.position, this.field.transform.transform.localScale));
    }

    private void placeFood() {
        this.placeGameObject(this.foodPrefab, this.randomPointInSquare(this.field.transform.position, this.field.transform.transform.localScale));
    }
}
