using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[SelectionBase]
[RequireComponent(typeof(Genes))]
[RequireComponent(typeof(Hunger))]
[RequireComponent(typeof(Thirst))]
[RequireComponent(typeof(Reproduce))]
public class StateController : MonoBehaviour {
    public Vector3? destination;

    [SerializeField]
    private State currentState;
    [HideInInspector] public float stateTimeElapsed;

    private Genes genes;
    private Hunger hunger;
    private Thirst thirst;
    private Reproduce reproduce;
    private WanderAction wander;

    [SerializeField]
    private Text text;

    void Start() {
        this.genes = GetComponent<Genes>();

        this.hunger = GetComponent<Hunger>();
        this.thirst = GetComponent<Thirst>();
        this.reproduce = GetComponent<Reproduce>();

        if(this.text != null) {
            this.text.text = this.currentState.name;
        }
    }

    void Update() {
        this.currentState.updateState(this);
    }

    public Genes getGenes() {
        return this.genes;
    }

    public void transitionToState(State nextState) {
        currentState = nextState;

        if (this.text != null) {
            this.text.text = nextState.name;
        }

        onExitState();
    }

    public bool checkIfCountDownElapsed(float duration) {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    public Vector3 randomNavCircle(float distance) {
        Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * distance;

        Debug.Log("Made a randomDirection " + randomDirection);

        // Converting the Vector3 from a X Y value to only a X Z value
        return new Vector3(randomDirection.x, this.transform.position.y, randomDirection.y);
    }

    private void onExitState() {
        stateTimeElapsed = 0;
    }

    private void OnDrawGizmos() {
        this.currentState.drawGizmos();
    }
}
