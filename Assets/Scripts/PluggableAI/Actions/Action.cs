using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Action : ScriptableObject {
    public abstract void act(StateController controller, ActionState actionState);

    public Vector3 randomNavCircle(Vector3 currentPosition, float distance) {
        Vector3 randomDirection = new Vector2(currentPosition.x, currentPosition.z) + UnityEngine.Random.insideUnitCircle * distance;

        // Converting the Vector3 from a X Y value to only a X Z value
        return new Vector3(randomDirection.x, currentPosition.y, randomDirection.y);
    }

    public void drawGizmos(StateController stateController, ActionState actionState) {
        Color gizmoColor = Color.blue;

        if (stateController != null) {
            if (stateController.getGenes() != null) {
                UnityEditor.Handles.color = gizmoColor;
                UnityEditor.Handles.DrawWireDisc(stateController.transform.position, new Vector3(0, 1, 0), stateController.getGenes().radiusOfSight);
            }

            if (actionState.destination != null) {
                Gizmos.color = gizmoColor;
                Gizmos.DrawLine(stateController.transform.position, (Vector3)actionState.destination);
            }
        }
    }
}
