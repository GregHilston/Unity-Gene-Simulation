using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/HuntFoodAction")]
public class HuntFoodAction : Action {
    private GameObject foodTooEat;
    private StateController lastStateController;

    public override void act(StateController stateController, ActionState actionState) {
        if (stateController.getGenes() != null) {
            this.lastStateController = stateController;

            if (this.foodTooEat == null) {
                Collider[] hitColliders = Physics.OverlapSphere(stateController.transform.position, stateController.getGenes().radiusOfSight);
                for (int i = 0; i < hitColliders.Length; i++) {
                    if (hitColliders[i].gameObject.GetComponent<Vegetation>() != null) {
                        actionState.destination = hitColliders[i].gameObject.transform.position;
                        this.foodTooEat = hitColliders[i].gameObject;
                        break;
                    }
                }

                // We failed to find food, so we'll move somewhere random only if we've arrived at that random spot already
                if (actionState.destination == null) {
                    actionState.destination = this.randomNavCircle(stateController.transform.position, stateController.getGenes().radiusOfSight);
                }
            }

            float step = stateController.getGenes().movementSpeed * Time.deltaTime;
            stateController.transform.position = Vector3.MoveTowards(stateController.transform.position, (Vector3)actionState.destination, step);

            if (stateController.transform.position == actionState.destination) {
                actionState.destination = null;
                Destroy(this.foodTooEat);
                this.foodTooEat = null;

                Hunger hunger = stateController.GetComponent<Hunger>();
                if (hunger != null) {
                    hunger.decreaseHunger(20);
                }
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

            // if (huntFoodActionState.destination != null) {
                // Gizmos.color = gizmoColor;
                // Gizmos.DrawLine(this.lastStateController.transform.position, (Vector3)huntFoodActionState.destination);
            // }
        }
    }
}
