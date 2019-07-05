using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Genes))]
[RequireComponent(typeof(Hunger))]
[RequireComponent(typeof(Thirst))]
[RequireComponent(typeof(Reproduce))]
public class Metabolism : MonoBehaviour {
    private Genes genes;
    private Hunger hunger;
    private Thirst thirst;
    private Reproduce reproduce;

    void Start() {
        float initialDelay = 0.0f;
        float interval = 1.0f;

        this.genes = GetComponent<Genes>();
        this.hunger = GetComponent<Hunger>();
        this.thirst = GetComponent<Thirst>();
        this.reproduce = GetComponent<Reproduce>();

        InvokeRepeating("metabolize", initialDelay, interval);
    }

    void metabolize() {
        if (this.genes != null) {
            if (this.hunger != null) {
                this.hunger.increaseHunger(this.genes.hungerIncreasedPerMetabolismTick);
            }

            if (this.thirst != null) {
                this.thirst.increaseThirst(this.genes.thirstIncreasedPerMetabolismTick);
            }

            if (this.reproduce != null) {
                this.reproduce.increaseReproduce(this.genes.reproduceIncreasedPerMetabolismTick);
            }
        }
    }
}
