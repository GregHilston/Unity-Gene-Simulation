using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Action : ScriptableObject {
    public abstract void act(StateController controller, ActionState actionState);
    public abstract void drawGizmos(ActionState actionState);

    public Vector3 randomNavCircle(Vector3 currentPosition, float distance) {
        Vector3 randomDirection = new Vector2(currentPosition.x, currentPosition.z) + UnityEngine.Random.insideUnitCircle * distance;

        // Converting the Vector3 from a X Y value to only a X Z value
        return new Vector3(randomDirection.x, currentPosition.y, randomDirection.y);
    }
}
