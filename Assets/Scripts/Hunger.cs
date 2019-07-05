using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hunger : MonoBehaviour {
    [SerializeField]
    private int _maxHunger;

    [SerializeField]
    private int _currentHunger;

    [Space]
    [SerializeField]
    private UnityEvent onStarve;

    public int maxHunger {
        get {
            return this._maxHunger;
        }
    }

    public int currentHunger {
        get {
            return this._currentHunger;
        }
    }

    public void decreaseHungger(int amount) {
        if (amount <= 0) return;

        if (this.currentHunger > amount) {
        this._currentHunger -= amount;
        } else {
        this._currentHunger = 0;
            onStarve.Invoke();
            Destroy(gameObject);
        }
    }

    public void increaseHunger(int amount) {
        if (amount <= 0) return;
        
        var targetHunger = this._currentHunger + amount;
        if (targetHunger >= this._maxHunger) {
            this._currentHunger = this._maxHunger;
        } else {
            this._currentHunger = targetHunger;
        }
    }

}
