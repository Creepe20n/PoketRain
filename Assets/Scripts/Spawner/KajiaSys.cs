using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class KajiaSys : MonoBehaviour
{
    public List<GameObject> itemList = new(),enemyList = new();
    public List<E_Scr> eventList;
    [SerializeField] GameObject[] baseItms, baseEnemys;
    [SerializeField] Spawner spawner;
    public GameManager gameManager;

    [SerializeField] K_Scr easySettings, normalSettings, hardSettings;

    public int itemChance = 0, plusItemChance = 4,neededItemChance = 30,plusNeededItemChance = 2;
    private int startItemChance;

    bool gameStarted;

    [SerializeField] float minSpawnTime = 0.2f,minusSpawnTime = 0.02f,activeSpawnTime,startSpawnTime = 1f;
    float spawnClyle=0;

    [SerializeField] KajiaState activeKajiaState;

    [SerializeField] List<GameObject> objectPool = new();
    int eventCounter = 0;

    [SerializeField] GameObject eventAniObj;
    [SerializeField] Animator eventAni;
    [SerializeField] TextMeshProUGUI eventText;

    public void GameStart(P_Modifiy_scr modifiy) {
        activeKajiaState = KajiaState.easy;
        minSpawnTime = easySettings.minSpawnTime;

        itemList.AddRange(baseItms.ToList());
        enemyList.AddRange(baseEnemys.ToList());

        activeSpawnTime = startSpawnTime;

        itemChance = startItemChance = modifiy.K_startItemChance;
        plusItemChance += modifiy.K_startPlusItemChance;
        gameStarted = true;
        print("Kajia start");
        RunGame();
    }

    public void StopGame() {
        itemList.Clear();
        enemyList.Clear();

        spawnClyle = 0;
        gameStarted = false;
        try {
            CancelInvoke(nameof(RunGame));
        }
        catch { print("no Invoje"); }

        KillActiveObjects();
        ForceEndEvent();
        print("\nKajiaStoped");
    }

    void KillActiveObjects() {
        foreach(GameObject x in objectPool) {
            x.GetComponent<SO_Base>().KillObject();
        }
    }

     void RunGame() {
        if(!gameStarted)
            return;

        spawnClyle++;

        //Set State
        if(gameManager.score >= normalSettings.activateAtScore && gameManager.score < hardSettings.activateAtScore) {
            minSpawnTime = normalSettings.minSpawnTime;
            activeKajiaState = KajiaState.normal;
        }
        else if(gameManager.score >= hardSettings.activateAtScore) {
            minSpawnTime = hardSettings.minSpawnTime;
            activeKajiaState = KajiaState.hard;
        }

        //Set time
        if(activeSpawnTime > minSpawnTime)
            activeSpawnTime -= minusSpawnTime;

        //Drop
        if(CalcNextDrop()) {
            //Spawn Item
            neededItemChance += plusNeededItemChance;
            itemChance = startItemChance;
            spawner.NormalSpawner(itemList[Random.Range(0,itemList.Count)],spawner.CalcNextSpawnPoint(1,startSettings.spawnPoints),objectPool);
            RunGame();
        }
        else {
            //Enemy
            SpawnEnemyBase();
        }
    }

    bool CalcNextDrop() {
        //Sorgt dafür das es immer konstant seltener wird
        //ein Item zu bekommen, bleibt aber fair
        itemChance += plusItemChance;

        return itemChance >= neededItemChance;
    }

    void SpawnEnemyBase() {
        if(activeKajiaState == KajiaState.easy) {
            EasyEnemy();
        }
        if(activeKajiaState == KajiaState.normal) {
            NormalEnemy();
        }
        if(activeKajiaState == KajiaState.hard) {
            HardEnemy();
        }
        if(activeKajiaState == KajiaState.kill) {
            KillEnemy();
        }
    }
    [SerializeField] Start_Scr startSettings;
    void EasyEnemy() {
        //Random und Langsam
        spawner.NormalSpawner(enemyList[Random.Range(0,enemyList.Count)],
            startSettings.spawnPoints[Random.Range(0,startSettings.spawnPoints.Count)],objectPool);
        Invoke(nameof(RunGame),activeSpawnTime * (1 + (1 - SO_Base.SoTime)));
    }
    void NormalEnemy() {
        //Schneller und prezieser zusätzlich events

        if(CalcEvent(normalSettings.eventMax)) {
            eventCounter = 0;
            StartCoroutine(EventSpawner());
            return;
        }

        spawner.NormalSpawner(enemyList[Random.Range(0,enemyList.Count)],spawner.CalcNextSpawnPoint(normalSettings.distance,startSettings.spawnPoints)
            ,objectPool);

        Invoke(nameof(RunGame),activeSpawnTime * (1 + (1 - SO_Base.SoTime)));
    }

    Vector2 nextSpawnPoint = Vector2.zero;

    void HardEnemy() {
        if(CalcEvent(hardSettings.eventMax)) {
            eventCounter = 0;
            StartCoroutine(EventSpawner());
            return;
        }

        if(nextSpawnPoint != Vector2.zero) {
            spawner.NormalSpawner(enemyList[Random.Range(0,enemyList.Count)],nextSpawnPoint
            ,objectPool);
            nextSpawnPoint = Vector2.zero;
            Invoke(nameof(RunGame),activeSpawnTime * (1 + (1 - SO_Base.SoTime)));
            return;
        }

        //Normal hard Spawn
        Vector2 vec = spawner.CalcNextSpawnPoint(hardSettings.distance,startSettings.spawnPoints);
        nextSpawnPoint = spawner.CalcNextSpawnPoint(hardSettings.distance,startSettings.spawnPoints,false,vec);//Calc next

        spawner.NormalSpawner(enemyList[Random.Range(0,enemyList.Count)],vec
            ,objectPool);

        Invoke(nameof(RunGame),activeSpawnTime*(1+(1-SO_Base.SoTime)));
    }
    void KillEnemy() {

    }
    bool CalcEvent(int max) {
        eventCounter++;
        return eventCounter >= max;
    }

    IEvents activeEventI =null;
    GameObject eventObj;
    public IEnumerator EventSpawner() {
        //To create event and set up
        KillActiveObjects();

        E_Scr e = eventList[Random.Range(0,eventList.Count)];
        eventText.text = e.eventFrase;

        eventAniObj.SetActive(true);
        eventObj = Instantiate(e.eventObject);

        activeEventI = eventObj.GetComponent<IEvents>();

        yield return new WaitForSeconds(3);

        eventAni.Play("OutEventAni");

        yield return new WaitForSeconds(0.5f);

        eventAniObj.SetActive(false);

        activeEventI.StartEvent(startSettings,objectPool,this,activeSpawnTime,spawner,gameManager.playerBase);
    }
    public void ForceEndEvent() {
        //End the Event
        if(activeEventI != null) {
            activeEventI.EndEvent();
            Destroy(eventObj);
        }
    }
    public void PostEvent() {
        //To end Event and start normal Cycle
        activeEventI = null;
        Destroy(eventObj);
        RunGame();
    }
}

public enum KajiaState {
    easy,normal,hard,kill
}