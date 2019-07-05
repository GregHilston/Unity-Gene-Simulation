using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrimReaper : MonoBehaviour {
    private void onAnyDeath(GameObject gameObject) {
        Destroy(gameObject);
    }

    public void onStarve(GameObject gameObject) {
        this.onAnyDeath(gameObject);
    }

    public void onDehydration(GameObject gameObjec) {
        this.onAnyDeath(gameObject);
    }

    public void onLonliness(GameObject gameObjec) {
        this.onAnyDeath(gameObject);
    }
}
