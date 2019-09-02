using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/HuntWaterAction")]
public class HuntWaterAction : Action {
    public override void act(StateController stateController, ActionState actionState) {
        if (stateController.getGenes() != null) {
            if (actionState.waterToDrink == null) {
                Collider[] hitColliders = Physics.OverlapSphere(stateController.transform.position, stateController.getGenes().radiusOfSight);
                for(int i = 0; i < hitColliders.Length; i++) {
                    if(hitColliders[i].gameObject.GetComponent<Water>() != null) {
                        actionState.destination = hitColliders[i].gameObject.transform.position;
                        actionState.waterToDrink = hitColliders[i].gameObject;
                        break;
                    }
                }

                // We failed to find water, so we'll move somewhere random only if we've arrived at that random spot already
                if(actionState.destination == null) {
                    actionState.destination = this.randomNavCircle(stateController.transform.position, stateController.getGenes().radiusOfSight);
                }
            }

            float step = stateController.getGenes().movementSpeed * Time.deltaTime;
            stateController.transform.position = Vector3.MoveTowards(stateController.transform.position, (Vector3)actionState.destination, step);

            // found water
            if (stateController.transform.position == actionState.destination && actionState.waterToDrink != null && actionState.waterToDrink.transform.position == actionState.destination) {
                Thirst thirst = stateController.GetComponent<Thirst>();
                if(thirst != null) {
                    thirst.decreaseThirst(20);
                }
            }

            // arrived at destination
            if (stateController.transform.position == actionState.destination) {
                actionState.destination = null;
                Destroy(actionState.waterToDrink);
                actionState.waterToDrink = null;
            }
        }
    }
}
