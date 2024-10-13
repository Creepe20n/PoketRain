using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    [SerializeField] float speed, maxSpeed = 2;
    [SerializeField] int damage;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool colPlayer;

    private void OnCollisionEnter2D(Collision2D collision) {
        if(colPlayer) {
            try {
                collision.gameObject.GetComponent<P_Hit>().hitPlayer(changeLife:damage);
            }
            catch { }
        }
        gameObject.SetActive(false);
    }

    private void FixedUpdate() {
        transform.Translate(0,speed * SO_Base.SoTime * Time.deltaTime,0);
    }
}
