using UnityEngine;

public class EventBase:MonoBehaviour, I_Manager {
    public virtual void OnEventEnd() {
        throw new System.NotImplementedException();
    }

    public virtual void OnEventStart() {
        throw new System.NotImplementedException();
    }

    public virtual void OnGameEnd() {
        throw new System.NotImplementedException();
    }
    
    public virtual void OnGameStart() {
        throw new System.NotImplementedException();
    }
}
