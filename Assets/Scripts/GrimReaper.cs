﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimReaper : MonoBehaviour {
    private void onAnyDeath(GameObject gameObject) {
        Destroy(gameObject);
    }

    public void onStarve(GameObject gameObject) {
        Debug.Log("onStarve");

        this.onAnyDeath(gameObject);
    }

    public void onDehydration(GameObject gameObjec) {
        Debug.Log("onDehydration");

        this.onAnyDeath(gameObject);
    }

    public void onLonliness(GameObject gameObjec) {
        Debug.Log("onLonliness");

        this.onAnyDeath(gameObject);
    }
}
