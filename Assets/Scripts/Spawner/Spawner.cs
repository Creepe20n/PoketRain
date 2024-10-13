using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastThings.FastGameObject;
using FastThings.FastBase;
public class Spawner : MonoBehaviour
{
    [SerializeField] GameManager gameManager;


    public void NormalSpawner(GameObject spawnObject,Vector2 spawnPoint,List<GameObject> objectPool) {
        GameObject tempSpawnObject = F_GameObject.GetObjectFromPoolTag(objectPool.ToArray(),spawnObject);

        if(tempSpawnObject == null) {
            tempSpawnObject = Instantiate(spawnObject,spawnPoint,Quaternion.identity);
            tempSpawnObject.GetComponent<SO_Base>().gameManager = gameManager;
            objectPool.Add(tempSpawnObject);
            return;
        }

        tempSpawnObject.transform.position = spawnPoint;
        tempSpawnObject.SetActive(true);
    }
    public Vector2 CalcNextSpawnPoint(float inDistance,List<Vector2> spawnPoints,bool returnIndex = false,Vector2 alternativeOrigin = new()) {
        //Will return a random Spawn point that is in "inDistance" distance

        Vector2 xDistanceVec = alternativeOrigin != Vector2.zero ? alternativeOrigin : gameManager.playerBase.gameObject.transform.position;

        List<Vector2> tempSpawnPoints = new(spawnPoints);

        for(int i = 0;i < tempSpawnPoints.Count;i++) {
            if(Fast.X_Distance(tempSpawnPoints[i],xDistanceVec) > inDistance) {
                tempSpawnPoints.RemoveAt(i);
            }
        }

        if(returnIndex)
            return new Vector2(Random.Range(0,tempSpawnPoints.Count),0);

        return tempSpawnPoints[Random.Range(0,tempSpawnPoints.Count)];
    }
}