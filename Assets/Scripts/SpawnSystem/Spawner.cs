using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] StartData_Scr start_Data;
    [SerializeField] GameManager gameManager;
    int lastSpawnNum;
    public void SpawnObject(GameObject spawnObject,Vector2 spawnPos = new(),bool selfDecide = false) {
        if(!selfDecide) {
            Instantiate(spawnObject,spawnPos,Quaternion.identity);
            return;
        }


        Instantiate(spawnObject,start_Data.SpawnPoints[SpawnOnNum()],Quaternion.identity);
    }
    private int SpawnOnNum() {
        
        System.Collections.Generic.List<Vector2> altSpawnPos = start_Data.SpawnPoints;
        altSpawnPos.RemoveAt(lastSpawnNum);



        return 0;
    }
}
