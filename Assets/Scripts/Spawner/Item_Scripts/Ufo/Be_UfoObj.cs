using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastThings.FastBase;
public class Be_UfoObj:MonoBehaviour, P_Hit, I_USO {
    [SerializeField] float speed;
    [SerializeField] LayerMask wallMask;
    GameObject plyr;
    float minX =-10, maxX=10,y;
    [SerializeField]Vector2 moveTo;
    int scorePoints;
    int life = 5,activeLife;
    private void OnDisable() {
        plyr = null;
        StopCoroutine(SelectPos());
    }

    public void hitPlayer(int addScorePoints,float changeSpeed,int changeLife,GameManager gM) {
        gM.AddScorePoints(scorePoints);
        activeLife--;

        if(activeLife <= 0)
            gameObject.SetActive(false);
    }

    public void ResetObj(Vector2 collPos) {
    }

    public void SetPlayer(GameObject plyr) {
        if(this.plyr != null)
            return;

        activeLife = life;
        this.plyr = plyr;
        y = plyr.transform.position.y + 1.5f;
        transform.position = new Vector2(plyr.transform.position.x,y);
        StartCoroutine(SelectPos());
    }
    private void FixedUpdate() {
        transform.position = new Vector2(transform.position.x,y);
        int Xdir = moveTo.x < transform.position.x ? -1 : 1;

        if(WallCheck(Xdir) || Fast.X_Distance(moveTo,transform.position) < 0.1f) {
            return;
        }

        transform.Translate(Xdir * speed * Time.deltaTime,0,0);
    }

    IEnumerator SelectPos() {
        if(plyr == null)
            yield return null;

        moveTo = CalcMoveToPos();

        yield return new WaitForSeconds(1f);

        if(plyr != null)
            StartCoroutine(SelectPos());
    }


    bool WallCheck(int dir) {
        bool con = Fast.LookInDir(transform.position,new Vector2(dir,0),0.6f,wallMask);

        if(dir < 0 && con)
            minX = transform.position.x;

        if(dir > 0 && con)
            maxX = transform.position.x;

        return con;
    }

    Vector2 CalcMoveToPos() {
        return new Vector2(Random.Range(minX,maxX),0);
    }

    public void AddScore(int points) {
        scorePoints = points;
    }
}
