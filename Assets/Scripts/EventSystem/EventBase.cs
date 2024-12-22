using UnityEngine;

public class EventBase:MonoBehaviour, I_Manager {
    public virtual void OnEventEnd() {
        gameObject.SetActive(false);
    }

    public virtual void OnEventStart(GameObject hitObject,GameManager gameManager) {
        throw new System.NotImplementedException();
    }

    public virtual void OnGameEnd() {
        throw new System.NotImplementedException();
    }
    
    public virtual void OnGameStart() {
        throw new System.NotImplementedException();
    }
}
