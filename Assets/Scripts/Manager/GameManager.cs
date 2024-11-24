using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    private void Awake() {
        Application.targetFrameRate = 60;
    }
}
