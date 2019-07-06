using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : ScriptableObject {
    private List<GameEventListener> gameEventListeners = new List<GameEventListener>();

    public void Raise() {
        // Backwards so the lastest to register gets notified first
        for (int i = this.gameEventListeners.Count - 1; i >= 0; i--) {
            gameEventListeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener gameEventListener) {
        this.gameEventListeners.Add(gameEventListener);
    }

    public void UnregisterListener(GameEventListener gameEventListener) {
        this.gameEventListeners.Remove(gameEventListener);
    }
}
