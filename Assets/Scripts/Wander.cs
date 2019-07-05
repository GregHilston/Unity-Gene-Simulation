using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Genes))]
public class Wander : MonoBehaviour {
    private Genes genes;
    private Vector3? destination;

    public Vector3 randomNavCircle(Vector3 origin, float distance, int layermask) {
        Vector3 randomDirection = UnityEngine.Random.insideUnitCircle * distance;
        
        // Converting the Vector3 from a X Y value to only a X Z value

        return new Vector3(randomDirection.x, this.transform.position.y, randomDirection.y);
    }

    void Start() {
        this.genes = GetComponent<Genes>();
    }

    void Update() {
        if (this.genes != null) {
            if (this.destination == null) {
                int allLayers = -1;
                this.destination = this.randomNavCircle(this.transform.position, this.genes.radiusOfSight, allLayers);
            }

            float step = this.genes.movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, (Vector3)this.destination, step);

            if (this.transform.position == this.destination) {
                this.destination = null;
            }
        }
    }



    void OnDrawGizmosSelected() {
        Color gizmoColor = Color.blue;

        if (this.genes != null) {
            UnityEditor.Handles.color = gizmoColor;
            UnityEditor.Handles.DrawWireDisc(this.transform.position, new Vector3(0, 1, 0), this.genes.radiusOfSight);
        }

        if (this.destination != null) {
            Gizmos.color = gizmoColor;
            Gizmos.DrawLine(transform.position, (Vector3)destination);
        }
    }
}
