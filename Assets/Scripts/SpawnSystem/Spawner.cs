using UnityEngine;
using System.Collections.Generic;
public class Spawner : MonoBehaviour
{
    [SerializeField] StartData_Scr start_Data;
    [SerializeField] GameManager gameManager;
    int lastSpawnNum;

    List< GameObject> objetPool = new List< GameObject>();

    public void SpawnObject(GameObject spawnObject,Vector2 spawnPos = new(),bool selfDecide = false) {

        GameObject poolObj =  GetObjectFromPool(spawnObject,objetPool);

        if(selfDecide) {
            spawnPos = start_Data.SpawnPoints[SpawnOnNum()];
        }

        if(poolObj == null)
            Instantiate(spawnObject,spawnPos,Quaternion.identity);
        else {
            poolObj.transform.position = spawnPos;
            poolObj.SetActive(true);
        }
    }

    public GameObject GetObjectFromPool(GameObject searchObject,List<GameObject> pool) {
        
        string tag = searchObject.tag;

        for(int i = 0; i < pool.Count;i++) {
            if(!pool[i].activeInHierarchy && pool[i].tag == tag) {
                return pool[i];
            }
        }

        return null;
    }

    private int SpawnOnNum() {
        
        System.Collections.Generic.List<Vector2> altSpawnPos = start_Data.SpawnPoints;
        altSpawnPos.RemoveAt(lastSpawnNum);

        switch(gameManager.activeDifficulty) {
            case E_ActiveDifficulty.easy:
                return Random.Range(0, altSpawnPos.Count);
            case E_ActiveDifficulty.normal:
                return GetSpawnPointInDistance(gameManager.player.transform.position,2,start_Data.SpawnPoints);
            case E_ActiveDifficulty.hard:
                return GetSpawnPointInDistance(gameManager.player.transform.position,1f,start_Data.SpawnPoints);
            case E_ActiveDifficulty.kill:
                return GetSpawnPointInDistance(gameManager.player.transform.position,3f,start_Data.SpawnPoints);

        }

        return 0;
    }
    //Get a spawnpoint based on the distance of the center 
    public int GetSpawnPointInDistance(Vector2 centerPos,float distance,System.Collections.Generic.List<Vector2> points) {
        System.Collections.Generic.List<int> validOptions = new();

        for(int i = 0; i < points.Count;i++) {
            if(PoketAPI.Convert.ConvertPosition.X_Distance(centerPos,points[i]) <= distance)
                validOptions.Add(i);
        }

        return Random.Range(0, validOptions.Count);
    }
}
