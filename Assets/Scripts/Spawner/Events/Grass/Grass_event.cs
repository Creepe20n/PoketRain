using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastThings.FastCamera;
public class Grass_event:MonoBehaviour, IEvents {
    [SerializeField] int dropsTilEnd = 100;
    [SerializeField] float minSpawnTime = 0.4f, startSpawnTime = 1;
    [SerializeField] GameObject preGrassObj,grassObj,preRainDrop;
    List<GameObject> objectPool;
    List<Vector2> spawnPoints;
    Spawner spawner;
    KajiaSys kajiaSys;
    Transform playrTrans;
    [SerializeField]float groundY, grassBounds,activeSpawnTime;
    bool eventStarted;
    int droped = 0;

    public void EndEvent() {
        eventStarted = false;
        Destroy(grassObj);
        kajiaSys.PostEvent();
    }

    public void StartEvent(Start_Scr _startSettings,List<GameObject> _activeObjectPool,KajiaSys _kajiaSys,float _activeSpawnTime = 0,Spawner _spawner = null,P_Base _playerBase = null) {
        objectPool = _activeObjectPool;
        spawnPoints = _startSettings.spawnPoints;
        spawner = _spawner;
        kajiaSys = _kajiaSys;
        groundY = _playerBase.gameObject.transform.position.y;
        groundY -= _playerBase.gameObject.GetComponent<SpriteRenderer>().bounds.extents.y;

        playrTrans = _playerBase.transform;

        Vector2 grassPos = new Vector2(0,0-F_Camera.GetCameraHight(Camera.main)/2);
        grassObj = Instantiate(preGrassObj,grassPos,Quaternion.identity);
        grassBounds = grassObj.GetComponent<SpriteRenderer>().bounds.extents.y;
        eventStarted = true;

        activeSpawnTime = startSpawnTime;

        StartCoroutine(Game());
    }



    void FixedUpdate() {
        if(!eventStarted)
            return;

        if(groundY > grassObj.transform.position.y - grassBounds) {
            grassObj.transform.position = new Vector2(grassObj.transform.position.x,grassObj.transform.position.y + 0.2f*Time.deltaTime);
        }
    }

    IEnumerator Game() {
        if(!eventStarted)
            yield return null;
        yield return new WaitForSeconds(1);

        spawner.NormalSpawner(preRainDrop,spawner.CalcNextSpawnPoint(0.5f,spawnPoints),objectPool);
        droped++;

        if(activeSpawnTime >= minSpawnTime) {
            activeSpawnTime -= 0.1f;
        }

        if(droped >= dropsTilEnd) {

            EndEvent();
            yield return null;
        }
        
        StartCoroutine(Game());
    }


}
