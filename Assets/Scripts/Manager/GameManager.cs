using UnityEngine;
using PoketAPI.Touch;
using UnityEngine.Events;
public class GameManager : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    public E_ActiveDifficulty activeDifficulty;
    public int score = 0,highScore = 0;
    public bool blockGameStart;
    public UnityEvent onGameStartEvents,onGameEndEvents;

    private void Awake() {
        Application.targetFrameRate = 60;
    }

    private void Start() {
        
    }

    private void Update() {
        if(blockGameStart)
            return;

        if(GetTab.DoubleTab()) {
            StartGame();
        }
    }

    public void StartGame() {
        activeDifficulty = E_ActiveDifficulty.easy;
        score = 0;

        onGameStartEvents.Invoke();
        blockGameStart = true;
        print("Game Start Finish");
    }

    private void EndGame() {
        
        onGameEndEvents.Invoke();
        blockGameStart = false;
    }
}
