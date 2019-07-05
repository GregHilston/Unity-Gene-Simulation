using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GrimReaper))]
public class Reproduce : MonoBehaviour {
    [SerializeField]
    private int _maxReproduce;

    [SerializeField]
    private int _currentReproduce;

    [Space]
    [SerializeField]
    private UnityEvent onLonliness;

    public int maxReproduce {
        get {
            return this._maxReproduce;
        }
    }

    public int currentReproduce {
        get {
            return this._currentReproduce;
        }
    }

    public void decreaseHungger(int amount) {
        if (amount <= 0) return;

        if (this._currentReproduce > amount) {
            this._currentReproduce -= amount;
        }
    }

    public void increaseReproduce(int amount) {
        if (amount <= 0) return;

        var targetReproduce = this._currentReproduce + amount;
        if (targetReproduce >= this._maxReproduce) {
            this._currentReproduce = 0;
            onLonliness.Invoke();
        } else {
            this._currentReproduce = targetReproduce;
        }
    }

}
