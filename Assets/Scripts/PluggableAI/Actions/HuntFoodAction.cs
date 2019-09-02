using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/HuntFoodAction")]
public class HuntFoodAction : Action {
    public override void act(StateController stateController, ActionState actionState) {
        if (stateController.getGenes() != null) {
            if (actionState.foodToEat == null) {
                Collider[] hitColliders = Physics.OverlapSphere(stateController.transform.position, stateController.getGenes().radiusOfSight);
                for (int i = 0; i < hitColliders.Length; i++) {
                    if (hitColliders[i].gameObject.GetComponent<Vegetation>() != null) {
                        actionState.destination = hitColliders[i].gameObject.transform.position;
                        actionState.foodToEat = hitColliders[i].gameObject;
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
                Destroy(actionState.foodToEat);
                actionState.foodToEat = null;

                Hunger hunger = stateController.GetComponent<Hunger>();
                if (hunger != null) {
                    hunger.decreaseHunger(20);
                }
            }
        }
    }
}
