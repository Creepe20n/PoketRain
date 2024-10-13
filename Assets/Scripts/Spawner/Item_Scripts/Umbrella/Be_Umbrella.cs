using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Be_Umbrella:MonoBehaviour, I_USO,P_Hit {

    [SerializeField] float time = 10;
    GameObject plyer;

    public void AddScore(int points) {
    }

    public void hitPlayer(int addScorePoints = 0,float changeSpeed = 0,int changeLife = 0,GameManager gM = null) {
        gM.AddScorePoints(addScorePoints);
    }

    public void ResetObj(Vector2 collPos) {

    }

    public void SetPlayer(GameObject plyr) {
        plyer = plyr;
        StartCoroutine(existTime());
    }

    IEnumerator existTime() {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    void Update() {

        if(plyer == null)
            return;

        transform.position = new Vector2(plyer.transform.position.x,plyer.transform.position.y+plyer.GetComponent<SpriteRenderer>().bounds.extents.y);

    }
}
