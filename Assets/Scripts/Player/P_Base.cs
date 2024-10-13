using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class P_Base : MonoBehaviour,P_Hit
{
    public P_scr settings;
    public float speed;
    public int life,maxLife;
    bool gameStarted;
    Vector2 startPositon;
    GameObject preGraveStone,graveStone;
    

    private void Start() {
        startPositon = transform.position;
        SetDeffultValues();
        preGraveStone = settings.graveStone;
    }
    void SetDeffultValues() {
        speed = settings.speed;
        life = settings.life;
        gameStarted = false;
    }
    public void GameStart(P_Modifiy_scr modifiy) {
        speed += modifiy.speed;
        life += modifiy.life;
        maxLife = modifiy.maxHeart;

        if(modifiy.graveStone != null)
            preGraveStone = modifiy.graveStone;

        if(graveStone != null)
            graveStone.SetActive(false);

        gameStarted = true;
    }
    public void EndGame() {
        startPositon = transform.position;
        SetDeffultValues();
    }
    private void Update() {
        if(!gameStarted)
            return;

        if(life <= 0) {

            if(graveStone == null)
                graveStone = Instantiate(preGraveStone,transform.position, Quaternion.identity);


            graveStone.transform.position = transform.position;
            graveStone.SetActive(true);

            gameObject.SetActive(false);
        }

        life = Mathf.Clamp(life,0,maxLife);
    }

    public void hitPlayer(int addScorePoints,float changeSpeed,int changeLife,GameManager gM) {
        speed += changeSpeed;
        life += changeLife;
        gM.AddScorePoints(addScorePoints);
    }
}