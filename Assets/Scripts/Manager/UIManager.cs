using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    public void StartButton() {
        if(!gameManager.blockGameStart) {
            gameManager.StartGame();
        }
    }
}
