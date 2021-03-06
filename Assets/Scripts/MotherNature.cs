﻿using System.Collections;
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
    GameObject waterParent;
    [SerializeField]
    GameObject foodPrefab;
    [SerializeField]
    GameObject foodParent;

    void Start() {
        InvokeRepeating("placeWater", 0.0f, this.secondsToPlaceWater);
        InvokeRepeating("placeFood", 0.0f, this.secondsToPlaceFood);
    }

    private Vector3 randomPointInSquare(Vector3 center, Vector3 size) {
       float groundFloor = 0.0f;
       return center + new Vector3(Random.value * size.x, groundFloor, Random.value * size.z);
    }

    private void placeGameObject(GameObject parent, GameObject prefab, Vector3 location) {
        if(prefab != null) {
            GameObject instantiatedGameObject = Instantiate(prefab, location, Quaternion.identity);
            instantiatedGameObject.transform.parent = parent.transform;
            Debug.Log("placed " + instantiatedGameObject.name + " at " + location);
        }
    }

    private void placeWater() {
        this.placeGameObject(this.waterParent, this.waterPrefab, this.randomPointInSquare(this.field.transform.position, this.field.transform.transform.localScale));
    }

    private void placeFood() {
        this.placeGameObject(this.foodParent, this.foodPrefab, this.randomPointInSquare(this.field.transform.position, this.field.transform.transform.localScale));
    }
}
