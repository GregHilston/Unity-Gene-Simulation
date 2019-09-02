using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/State")]
public class State : ScriptableObject {
    public Action[] actions;
    public Transition[] transitions;

    public void updateState(StateController controller, ActionState actionState) {
        doActions(controller, actionState);
        checkTransitions(controller);
    }

    private void doActions(StateController controller, ActionState actionState) {
        for (int i = 0; i < actions.Length; i++) {
            actions[i].act(controller, actionState);
        }
    }

    private void checkTransitions(StateController controller) {
        for (int i = 0; i < transitions.Length; i++) {
            bool decisionSucceeded = transitions[i].decision.decide(controller);

            if (decisionSucceeded) {
                controller.transitionToState(transitions[i].trueState);
                return;
            } else {
                controller.transitionToState(transitions[i].falseState);
            }
        }
    }

    public void drawGizmos() {
        for (int i = 0; i < actions.Length; i++) {
            actions[i].drawGizmos();
        }
    }
}