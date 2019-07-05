using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Genes))]
[RequireComponent(typeof(Hunger))]
public class Metabolism : MonoBehaviour {
    private Genes genes;
    private Hunger hunger;

    void Start() {
        float initialDelay = 0.0f;
        float interval = 1.0f;

        this.genes = GetComponent<Genes>();
        this.hunger = GetComponent<Hunger>();

        InvokeRepeating("metabolize", initialDelay, interval);
    }

    void metabolize() {
        if (this.genes != null) {
            if (this.hunger != null) {
                this.hunger.increaseHunger(this.genes.hungerIncreasedPerMetabolismTick);
            }
        }
    }
}
