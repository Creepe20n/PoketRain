using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] availablePlayerPrefs;
    [SerializeField] private int activePlayer = 0;
    [SerializeField] private GameManager gameManager;

    private void Start() {
        gameManager.player = Instantiate(availablePlayerPrefs[activePlayer],
            new Vector2(Camera.main.transform.position.x,Camera.main.transform.position.y),
            Quaternion.identity);
    }
}
