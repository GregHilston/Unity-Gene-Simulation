using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(GrimReaper))]
public class Thirst : MonoBehaviour {
    [SerializeField]
    private int _maxThirst;

    [SerializeField]
    private int _currentThirst;

    [Space]
    [SerializeField]
    private UnityEvent onDehydration;

    public int maxThirst {
        get {
            return this._maxThirst;
        }
    }

    public int currentThirst {
        get {
            return this._currentThirst;
        }
    }

    public void decreaseHungger(int amount) {
        if (amount <= 0) return;

        if (this._currentThirst > amount) {
            this._currentThirst -= amount;
        }
    }

    public void increaseThirst(int amount) {
        if (amount <= 0) return;

        var targetThirst = this._currentThirst + amount;
        if (targetThirst >= this._maxThirst) {
            this._currentThirst = 0;
            onDehydration.Invoke();
        } else {
            this._currentThirst = targetThirst;
        }
    }

}
