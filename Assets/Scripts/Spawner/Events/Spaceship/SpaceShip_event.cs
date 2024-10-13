using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastThings.FastCamera;
public class SpaceShip_event:MonoBehaviour, IEvents,P_Hit {

    Spawner spawner;
    List<Vector2> spawnPoints;
    [SerializeField] GameObject preSpaceShip,preSpaceEnemy,preGravitron,preRocket,preUfo;
    [SerializeField] P_scr shipSettings;
    [SerializeField] float spawnTime,ufoLife,ufoMoveSpeed;
    GameObject playerObj,spaceShip,ufoObj;
    [SerializeField]
    Sprite[] ufoStages;
    KajiaSys kajiaSys;
    P_Base playrBase;

    List<GameObject> objectPool,destroyPool =new();

    bool startEvent, ufoReached;

    float lifeStateChange,maxSpawnY,minSpawnY;

    public void EndEvent() {
        spaceShip.SetActive(false);

        playrBase.life = spaceShip.GetComponent<P_Base>().life;
        playrBase.gameObject.SetActive(true);
        kajiaSys.gameManager.playerBase = playrBase;

        foreach(GameObject x in destroyPool) {
            Destroy(x);
        }

        kajiaSys.PostEvent();
    }

    public void hitPlayer(int addScorePoints = 0,float changeSpeed = 0,int changeLife = 0,GameManager gM = null) {
        ufoLife -= changeLife;
    }

    public void StartEvent(Start_Scr _startSettings,List<GameObject> _activeObjectPool,KajiaSys _kajiaSys,float _activeSpawnTime = 0,Spawner _spawner = null,P_Base _playerBase = null){
        spawner = _spawner;
        playerObj = _playerBase.gameObject;
        spawnPoints = _startSettings.spawnPoints;
        kajiaSys = _kajiaSys;
        playrBase = _playerBase;
        objectPool = _activeObjectPool;

        shipSettings.life = _playerBase.life;

        lifeStateChange = ufoLife / ufoStages.Length;

        float overCamY = (F_Camera.GetCameraHight(Camera.main) / 2)+2;

        ufoObj = Instantiate(preUfo,new Vector2(0,overCamY),Quaternion.identity);

        minSpawnY = playerObj.GetComponent<SpriteRenderer>().bounds.extents.y;
        minSpawnY = playerObj.transform.position.y + minSpawnY + 0.5f;

        maxSpawnY = ufoObj.GetComponent<SpriteRenderer>().bounds.extents.y;
        maxSpawnY = 2 - (maxSpawnY + 0.5f);

        destroyPool.Add(ufoObj);

        startEvent = true;
    }

    IEnumerator PhaseOne() {
        yield return new WaitForSeconds(3);
        EndEvent();
    }

    private void FixedUpdate() {
        if(!startEvent)
            return;

        if(!ufoReached) {
            ufoObj.transform.Translate(0,-ufoMoveSpeed * Time.deltaTime,0);

            if(ufoObj.transform.position.y <= 2) {
                ufoReached = true;

                playerObj.SetActive(false);
                spaceShip = Instantiate(preSpaceShip,playerObj.transform.position,Quaternion.identity);
                spaceShip.GetComponent<P_Base>().maxLife = playrBase.maxLife;
                spaceShip.GetComponent<P_Base>().speed = playrBase.speed;
                kajiaSys.gameManager.playerBase = spaceShip.GetComponent<P_Base>();


                destroyPool.Add(spaceShip);

                StartCoroutine(PhaseOne());
            }

            return;
        }
    }
}
