using UnityEngine;
using System.Collections.Generic;
public class Kajia : MonoBehaviour,I_Manager
{
    [Header("Dependencies")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Spawner spawner;

    [Header("Diff Settings")]
    [SerializeField] private Scr_KajiaSettings easySettings;
    [SerializeField] private Scr_KajiaSettings normalSettings;
    [SerializeField] private Scr_KajiaSettings hardSettings;
    [SerializeField] private Scr_KajiaSettings killSettings;

    [Header("Active Kajia Values")]
    [SerializeField] private float spawnTime;
    [SerializeField] private float lowestSpawnTime;
    //
    [SerializeField] private List<GameObject> enemys;
    [SerializeField] private List<GameObject> items;
    [SerializeField] private List<GameObject> events;

    public void OnGameStart() {
        SpawnNext();
    }

    public void OnGameEnd() {
        CancelInvoke();
    }

    private void SpawnNext() {
        CheckDifficulty();
        SpawnDecide();

        Invoke(nameof(SpawnNext),spawnTime);
    }

    private void SpawnDecide() {
        
    }

    private void CheckDifficulty() {
        if(gameManager.score < easySettings.setUnderScore) {
            gameManager.activeDifficulty = E_ActiveDifficulty.easy;
            lowestSpawnTime = easySettings.newSpawnTimeLow;
            return;
        }
        if(gameManager.score < normalSettings.setUnderScore) {
            gameManager.activeDifficulty = E_ActiveDifficulty.normal;
            lowestSpawnTime = normalSettings.newSpawnTimeLow;
            return;
        }
        if(gameManager.score < hardSettings.setUnderScore) {
            gameManager.activeDifficulty = E_ActiveDifficulty.hard;
            lowestSpawnTime = hardSettings.newSpawnTimeLow;
            return;
        }
        if(gameManager.score < killSettings.setUnderScore) {
            gameManager.activeDifficulty = E_ActiveDifficulty.kill;
            lowestSpawnTime = killSettings.newSpawnTimeLow;
            return;
        }
    }

    public void OnEventEnd() {
        throw new System.NotImplementedException();
    }

    public void OnEventStart(GameObject hitObject,GameManager gameManager) {
        throw new System.NotImplementedException();
    }
}
