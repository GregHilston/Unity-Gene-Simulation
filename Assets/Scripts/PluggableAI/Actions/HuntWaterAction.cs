using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/HuntWaterAction")]
public class HuntWaterAction : Action {
    private Vector3? destination;
    private GameObject waterTooDrink;
    private StateController lastStateController;

    private Vector3 randomNavCircle(StateController stateController, Vector3 origin, float distance, int layermask) {
        Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * distance;

        // Converting the Vector3 from a X Y value to only a X Z value
        return new Vector3(randomDirection.x, stateController.transform.position.y, randomDirection.y);
    }

    public override void act(StateController stateController) {
        if (stateController.getGenes() != null) {
            this.lastStateController = stateController;

            if (this.waterTooDrink == null) {
                Collider[] hitColliders = Physics.OverlapSphere(stateController.transform.position, stateController.getGenes().radiusOfSight);
                for(int i = 0; i < hitColliders.Length; i++) {
                    if(hitColliders[i].gameObject.GetComponent<Water>() != null) {
                        this.destination = hitColliders[i].gameObject.transform.position;
                        this.waterTooDrink = hitColliders[i].gameObject;
                        break;
                    }
                }

                // We failed to find water, so we'll move somewhere random only if we've arrived at that random spot already
                if(this.destination == null) {
                    int allLayers = -1;
                    this.destination = this.randomNavCircle(stateController, stateController.transform.position, stateController.getGenes().radiusOfSight, allLayers);
                }
            }

            float step = stateController.getGenes().movementSpeed * Time.deltaTime;
            stateController.transform.position = Vector3.MoveTowards(stateController.transform.position, (Vector3)this.destination, step);

            if (stateController.transform.position == this.destination) {
                this.destination = null;
                Destroy(this.waterTooDrink);
                this.waterTooDrink = null;

                Thirst thirst = stateController.GetComponent<Thirst>();
                if(thirst != null) {
                    thirst.decreaseThirst(20);
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

            if (this.destination != null) {
                Gizmos.color = gizmoColor;
                Gizmos.DrawLine(this.lastStateController.transform.position, (Vector3)destination);
            }
        }
    }
}
