using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hunger))]
[RequireComponent(typeof(Thirst))]
[RequireComponent(typeof(Reproduce))]
public class Brain : MonoBehaviour {
    enum Activity {
        WANDERING,
        FOOD_SEARCH,
        WATER_SEARCH,
        MATE_SEARCH,
        EATING,
        DRINKING,
        BIRTHING
    }

    [SerializeField]
    private Activity currentActivity = Activity.WANDERING;
    private Hunger hunger;
    private Thirst thirst;
    private Reproduce reproduce;

    void Start() {
        this.hunger = GetComponent<Hunger>();
        this.thirst = GetComponent<Thirst>();
        this.reproduce = GetComponent<Reproduce>();
    }

    private Activity decideActivity() {
        if(this.hunger != null && this.thirst != null && this.reproduce != null) {
            if(this.hunger.currentHunger >= this.thirst.currentThirst && this.hunger.currentHunger >= this.reproduce.currentReproduce) {
                return Activity.FOOD_SEARCH;
            }

            if (this.thirst.currentThirst >= this.hunger.currentHunger && this.thirst.currentThirst >= this.reproduce.currentReproduce) {
                return Activity.WATER_SEARCH;
            }

            if (this.reproduce.currentReproduce >= this.hunger.currentHunger && this.reproduce.currentReproduce >= this.thirst.currentThirst) {
                return Activity.MATE_SEARCH;
            }
        }

        // Default cause
        return Activity.WANDERING;
    }

    void Update() {
        this.currentActivity = this.decideActivity();
    }
}
