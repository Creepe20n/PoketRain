using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SO_Base : MonoBehaviour
{
    public SO_scr settings;
    public static float SoTime = 1;
    [SerializeField] float axoloration, axolarationRate = 0.1f;

    [HideInInspector]public GameManager gameManager;

    ParticleSystem particels;
    new Collider2D collider;
    GameObject spawnObjectSave;

    IItem i_item;

    private void OnEnable() {
        collider.enabled = true;
    }

    private void Awake() {
        particels = Instantiate(settings.preParticle,transform.position,Quaternion.identity);
        particels.Stop();
        collider = GetComponent<Collider2D>();
        GetComponent<Rigidbody2D>().gravityScale = 0;

        i_item = gameObject?.GetComponent<IItem>();
    }

    public void KillObject() {
        if(spawnObjectSave != null)
            spawnObjectSave.SetActive(false);
        StopGame();
    }

    public void StopGame() {
        collider.enabled = false;
        particels.transform.position = transform.position;
        particels.Play();
        axoloration = 0;
        gameObject.SetActive(false);
    }

    private void FixedUpdate() {
        transform.Translate(0,-(SoTime * (settings.moveSpeed+axoloration) * Time.deltaTime),0);
        axoloration += axolarationRate*SoTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if(!collision.collider.CompareTag("Respawn"))
            return;

        try {
            PlayerHitCont(collision.collider.gameObject);
        }
        catch { }

        StopGame();
    }

    void PlayerHitCont(GameObject collisonObj) {
        GameObject plyr = collisonObj;
        P_Hit hit = plyr?.GetComponent<P_Hit>();

        if(hit == null) {
            gameManager.AddScorePoints(settings.changeScorePointsGround);
            i_item?.hitGround();
            return;
        }
        i_item?.hitPlyr();

        if(collisonObj != spawnObjectSave)
            hit.hitPlayer(settings.changeScorePointsPlayer,settings.changePlayerSpeed,settings.changeLife,gameManager);

        if(settings.spawnObject == null)
            return;

        I_USO temp;
        if(spawnObjectSave == null) {
            spawnObjectSave = Instantiate(settings.spawnObject,transform.position,Quaternion.identity);
            spawnObjectSave.SetActive(false);
            temp = spawnObjectSave.GetComponent<I_USO>();
        }
        else {
            temp = spawnObjectSave.GetComponent<I_USO>();
        }

        if(spawnObjectSave.activeInHierarchy)
            return;

        spawnObjectSave.SetActive(true);
        temp.SetPlayer(collisonObj);
        temp.AddScore(settings.changeScorePointsPlayer);
        temp.ResetObj(transform.position);
    }
}
