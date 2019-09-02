using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "PluggableAI/Actions/WanderAction")]
public class WanderAction : Action {
    public override void act(StateController stateController, ActionState actionState) {
        if (stateController.getGenes() != null) {
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
}
