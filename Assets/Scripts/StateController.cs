using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Genes))]
[RequireComponent(typeof(Hunger))]
[RequireComponent(typeof(Thirst))]
[RequireComponent(typeof(Reproduce))]
public class StateController : MonoBehaviour {
    [SerializeField]
    private State currentState;
    private Genes genes;
    private Hunger hunger;
    private Thirst thirst;
    private Reproduce reproduce;
    private WanderAction wander;

    void Start() {
        this.genes = GetComponent<Genes>();

        this.hunger = GetComponent<Hunger>();
        this.thirst = GetComponent<Thirst>();
        this.reproduce = GetComponent<Reproduce>();
    }

    void Update() {
        this.currentState.updateState(this);
    }

    public Genes getGenes() {
        return this.genes;
    }

    private void OnDrawGizmos() {
        this.currentState.DrawGizmos();
    }
}
