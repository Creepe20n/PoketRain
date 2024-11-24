using UnityEngine;

public class EntityBase : MonoBehaviour,I_Entity
{
    public int health = 3;
    public int moveSpeed = 3;
    public virtual void Hit(int damage,GameObject hitObject) {
        health -= damage;
    }

    public virtual void KillObject() {
        gameObject.SetActive(false);
    }
}
