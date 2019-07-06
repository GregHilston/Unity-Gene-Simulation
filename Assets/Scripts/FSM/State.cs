using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject {
    public Action[] actions;

    public void updateState(StateController controller) {
        doActions(controller);
    }

    private void doActions(StateController controller) {
        for (int i = 0; i < actions.Length; i++) {
            actions[i].act(controller);
        }
    }

    public void DrawGizmos() {
        for (int i = 0; i < actions.Length; i++) {
            actions[i].DrawGizmos();
        }
    }
}