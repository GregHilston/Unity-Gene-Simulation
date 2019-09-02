using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/WanderAction")]
public class WanderAction : Action {
    private StateController lastStateController;

    private Vector3 randomNavCircle(StateController stateController, Vector3 origin, float distance, int layermask) {
        Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * distance;
        
        // Converting the Vector3 from a X Y value to only a X Z value
        return new Vector3(randomDirection.x, stateController.transform.position.y, randomDirection.y);
    }

    public override void act(StateController stateController) {
        if (stateController.getGenes() != null) {
            this.lastStateController = stateController;

            if (stateController.destination == null) {
                int allLayers = -1;
                stateController.destination = this.randomNavCircle(stateController, stateController.transform.position, stateController.getGenes().radiusOfSight, allLayers);
            }

            float step = stateController.getGenes().movementSpeed * Time.deltaTime;
            stateController.transform.position = Vector3.MoveTowards(stateController.transform.position, (Vector3)stateController.destination, step);

            if (stateController.transform.position == stateController.destination) {
                stateController.destination = null;
            }
        }
    }

    public override void drawGizmos() {
        Color gizmoColor = Color.blue;

        if (this.lastStateController != null) {
            if (this.lastStateController.getGenes() != null) {
                UnityEditor.Handles.color = gizmoColor;
                UnityEditor.Handles.DrawWireDisc(this.lastStateController.transform.position, new Vector3(0, 1, 0), this.lastStateController.getGenes().radiusOfSight);
            }

            //if (stateController.destination != null) {
                //Gizmos.color = gizmoColor;
                //Gizmos.DrawLine(this.lastStateController.transform.position, (Vector3)destination);
            //}
        }
    }
}
