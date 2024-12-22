using UnityEngine;

public interface I_Manager
{
    public void OnGameStart();
    public void OnGameEnd();
    public void OnEventStart(GameObject hitObject,GameManager gameManager);
    public void OnEventEnd();
}
