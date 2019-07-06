using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hunger))]
public class DesiresDisplay : MonoBehaviour {
    private Hunger hunger;
    [SerializeField]
    private GameObject hungerBar;
    private Thirst thirst;
    [SerializeField]
    private GameObject thirstBar;
    private Reproduce reproduce;
    [SerializeField]
    private GameObject reproduceBar;

    private void Start() {
        this.hunger = GetComponent<Hunger>();
        this.thirst = GetComponent<Thirst>();
        this.reproduce = GetComponent<Reproduce>();
    }

    private void Update() {
        if(this.hunger != null && this.hungerBar != null) {
            this.hungerBar.transform.localScale = new Vector3(this.hungerBar.transform.localScale.x, (float)this.hunger.currentHunger/(float)this.hunger.maxHunger, this.hungerBar.transform.localScale.z);
        }

        if (this.thirst != null && this.thirstBar != null) {
            this.thirstBar.transform.localScale = new Vector3(this.thirstBar.transform.localScale.x, (float)this.thirst.currentThirst / (float)this.thirst.maxThirst, this.thirstBar.transform.localScale.z);
        }

        if (this.reproduce != null && this.reproduceBar != null) {
            this.reproduceBar.transform.localScale = new Vector3(this.reproduceBar.transform.localScale.x, (float)this.reproduce.currentReproduce / (float)this.reproduce.maxReproduce, this.reproduceBar.transform.localScale.z);
        }
    }
}
