using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/WanderAction")]
public class WanderAction : Action {
    private StateController lastStateController;

    public override void act(StateController stateController, ActionState actionState) {
        if (stateController.getGenes() != null) {
            this.lastStateController = stateController;

            if (actionState.destination == null) {
                actionState.destination = this.randomNavCircle(stateController.transform.position, stateController.getGenes().radiusOfSight);
            }

            float step = stateController.getGenes().movementSpeed * Time.deltaTime;
            stateController.transform.position = Vector3.MoveTowards(stateController.transform.position, (Vector3)actionState.destination, step);

            if (stateController.transform.position == actionState.destination) {
                actionState.destination = null;
            }
        }
    }

    public override void drawGizmos(ActionState actionState) {
        Color gizmoColor = Color.blue;

        if (this.lastStateController != null) {
            if (this.lastStateController.getGenes() != null) {
                UnityEditor.Handles.color = gizmoColor;
                UnityEditor.Handles.DrawWireDisc(this.lastStateController.transform.position, new Vector3(0, 1, 0), this.lastStateController.getGenes().radiusOfSight);
            }

            if (actionState.destination != null) {
                Gizmos.color = gizmoColor;
                Gizmos.DrawLine(this.lastStateController.transform.position, (Vector3)actionState.destination);
            }
        }
    }
}
