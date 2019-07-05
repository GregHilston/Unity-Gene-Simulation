using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hunger))]
public class DesiresDisplay : MonoBehaviour {
    private Hunger hunger;
    [SerializeField]
    private GameObject hungerBar;

    private void Start() {
        this.hunger = GetComponent<Hunger>();
    }

    private void Update() {
        if(this.hunger != null && this.hungerBar != null) {
            this.hungerBar.transform.localScale = new Vector3(this.hungerBar.transform.localScale.x, (float)this.hunger.currentHunger/(float)this.hunger.maxHunger, this.hungerBar.transform.localScale.z);
        }
    }
}
