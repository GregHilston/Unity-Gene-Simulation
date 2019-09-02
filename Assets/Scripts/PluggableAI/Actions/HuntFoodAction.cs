using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/HuntFoodAction")]
public class HuntFoodAction : Action {
    private GameObject foodTooEat;
    private StateController lastStateController;

    private Vector3 randomNavCircle(StateController stateController, Vector3 origin, float distance, int layermask) {
        Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * distance;

        // Converting the Vector3 from a X Y value to only a X Z value
        return new Vector3(randomDirection.x, stateController.transform.position.y, randomDirection.y);
    }

    public override void act(StateController stateController) {
        if (stateController.getGenes() != null) {
            this.lastStateController = stateController;

            if (this.foodTooEat == null) {
                Collider[] hitColliders = Physics.OverlapSphere(stateController.transform.position, stateController.getGenes().radiusOfSight);
                for (int i = 0; i < hitColliders.Length; i++) {
                    if (hitColliders[i].gameObject.GetComponent<Vegetation>() != null) {
                        stateController.destination = hitColliders[i].gameObject.transform.position;
                        this.foodTooEat = hitColliders[i].gameObject;
                        break;
                    }
                }

                // We failed to find food, so we'll move somewhere random only if we've arrived at that random spot already
                if (stateController.destination == null) {
                    int allLayers = -1;
                    stateController.destination = stateController.randomNavCircle(stateController.getGenes().radiusOfSight);
                }
            }

            float step = stateController.getGenes().movementSpeed * Time.deltaTime;
            stateController.transform.position = Vector3.MoveTowards(stateController.transform.position, (Vector3)stateController.destination, step);

            if (stateController.transform.position == stateController.destination) {
                stateController.destination = null;
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

            // if (this.destination != null) {
                // Gizmos.color = gizmoColor;
                // Gizmos.DrawLine(this.lastStateController.transform.position, (Vector3)destination);
            // }
        }
    }
}
