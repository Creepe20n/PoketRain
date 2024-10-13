using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sin_event : MonoBehaviour,IEvents
{

    [SerializeField] int rounds = 3;
    [SerializeField] GameObject rainDrop;

    Spawner spawner;
    Start_Scr startSettings;
    KajiaSys kajiaSys;
    P_Base playerBase;
    List<GameObject> objectPool;


    public void StartEvent(Start_Scr _startSettings,List<GameObject> _activeObjectPool,KajiaSys _kajiaSys,float _activeSpawnTime = 0,Spawner _spawner = null
        ,P_Base _playerBase = null) 
    {
        spawner = _spawner;
        startSettings = _startSettings;
        kajiaSys = _kajiaSys;
        playerBase = _playerBase;
        objectPool = _activeObjectPool;

        StartCoroutine(EventRunner());
    }

    public void EndEvent() {
        kajiaSys.PostEvent();
    }


    int runnedRounds = 0;
    IEnumerator EventRunner() {
        //Calc empty Spawn points
        int freeSlotIndex = (int)spawner.CalcNextSpawnPoint(2,startSettings.eventSpawnPoints,true).x;
        int freeSlotTow = 0;
        //Look for place for secon spawn point
        if(freeSlotIndex + 1 < startSettings.eventSpawnPoints.Count - 1) {
            freeSlotTow = freeSlotIndex + 1;
        }else if(freeSlotIndex-1 > -1) {
            freeSlotTow=freeSlotIndex-1;
        }
        //Spawn forshadow for free
        spawner.NormalSpawner(rainDrop,startSettings.eventSpawnPoints[freeSlotIndex],objectPool);

        yield return new WaitForSeconds(2);
        int mMod = Random.Range(0,2);
        //Spawn Wave
        for(int i = 0; i < startSettings.eventSpawnPoints.Count;i++) {
            if(i == freeSlotIndex || i == freeSlotTow) {
                continue;
            }
            Vector2 spawnPos = new Vector2(startSettings.eventSpawnPoints[i].x,startSettings.spawnPoints[i].y+plusY(mMod,i));
            spawner.NormalSpawner(rainDrop,spawnPos,objectPool);
        }

        //Go trugh rounds
        runnedRounds++;
        yield return new WaitForSeconds(2);

        if(runnedRounds >= rounds)
            EndEvent();
        StartCoroutine(EventRunner());
    }

    float plusY(int mMod,int activeIndex) {

        float returnVal = 0;
        switch(mMod){
            case 0:
                returnVal = Mathf.Sin(startSettings.eventSpawnPoints[activeIndex].x);
                break;
            case 1:
                returnVal = Mathf.Cos(startSettings.eventSpawnPoints[activeIndex].x);
                break;
            case 2:
                returnVal = Mathf.Tan(startSettings.eventSpawnPoints[activeIndex].x);
                break;
            default:
                returnVal = Mathf.Sin(startSettings.eventSpawnPoints[activeIndex].x);
                break;
        }
        return returnVal;
    }
}
