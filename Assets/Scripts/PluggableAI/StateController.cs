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
    public State remainState;
    [HideInInspector] public float stateTimeElapsed;

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

    public void transitionToState(State nextState) {
        if (nextState != remainState) {
            currentState = nextState;
            onExitState();
        }
    }

    public bool checkIfCountDownElapsed(float duration) {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void onExitState() {
        stateTimeElapsed = 0;
    }

    private void OnDrawGizmos() {
        this.currentState.drawGizmos();
    }
}
