using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    public E_ActiveDifficulty activeDifficulty;
    public int score = 0,highScore = 0;

    private void Awake() {
        Application.targetFrameRate = 60;
    }

    private void Start() {
        
    }

    private void Update() {
        
    }

    private void StartGame() {
        activeDifficulty = E_ActiveDifficulty.easy;
        score = 0;
    }

    private void EndGame() {
    
    }
}
