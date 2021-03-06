﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
[RequireComponent(typeof(Genes))]
[RequireComponent(typeof(Hunger))]
[RequireComponent(typeof(Thirst))]
[RequireComponent(typeof(Reproduce))]
public class StateController : MonoBehaviour {
    [SerializeField]
    private State currentState;
    [HideInInspector] public float stateTimeElapsed;

    private Genes genes;
    private Hunger hunger;
    private Thirst thirst;
    private Reproduce reproduce;
    private ActionState actionState = new ActionState();

    [SerializeField]
    private Text nameText;

    [SerializeField]
    private Text currentStateText;

    void Start() {
        this.genes = GetComponent<Genes>();

        this.hunger = GetComponent<Hunger>();
        this.thirst = GetComponent<Thirst>();
        this.reproduce = GetComponent<Reproduce>();

        if(this.nameText != null) {
            this.nameText.text = this.gameObject.transform.name;
        }

        if(this.currentStateText != null) {
            this.currentStateText.text = this.currentState.name;
        }
    }

    void Update() {
        this.currentState.updateState(this, this.actionState);
    }

    public Genes getGenes() {
        return this.genes;
    }

    public void transitionToState(State nextState) {
        currentState = nextState;

        if (this.currentStateText != null) {
            this.currentStateText.text = nextState.name;
        }

        onExitState();
    }

    public bool checkIfCountDownElapsed(float duration) {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void onExitState() {
        stateTimeElapsed = 0;
    }

    private void OnDrawGizmos() {
        this.currentState.drawGizmos(this, this.actionState);
    }
}
