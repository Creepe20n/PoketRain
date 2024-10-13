using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastThings.FastTouch;
using FastThings.FastGameObject;
public class ShootOnTab : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject bullet;

    List<GameObject> bullets = new();
    bool block = false;


    private void Update() {

        if(!F_Touch.IsTouching())
            block = false;

        if(F_Touch.DoubleTab() && !block) {
            GameObject tempBullet = F_GameObject.GetObjectFromPoolTag(bullets.ToArray(),bullet);

            if(tempBullet != null) {
                tempBullet.transform.position = spawnPoint.position;
                tempBullet.SetActive(true);
                return;
            }

            bullets.Add(Instantiate(bullet, spawnPoint.position,Quaternion.identity));
            block = true;
        }
    }

    private void OnDestroy() {
        foreach(GameObject x in  bullets) {
            Destroy(x);
        }
    }
}
