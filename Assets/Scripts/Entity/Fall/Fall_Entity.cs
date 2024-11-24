using UnityEngine;

public class Fall_Entity : EntityBase
{
   public static float moveTime = 1f;

    public void FixedUpdate() {
        transform.Translate(0,moveTime*moveSpeed*Time.deltaTime,0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("Player")) {

        }
        if(collision.collider.CompareTag("Respawn")) {

        }
    }
}
