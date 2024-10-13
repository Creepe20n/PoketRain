using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FastThings.FastTouch;
using FastThings.FastBase;
public class P_Move : MonoBehaviour
{
    [SerializeField] P_Base p_Base;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;

    private void FixedUpdate() {
        Vector2 touchPos = F_Touch.TouchPos();

        if(Fast.X_Distance(transform.position,touchPos) < 0.1f)
            return;
        if(touchPos == Vector2.zero)
            return;
        if(WallControll(touchPos))
            return;

        spriteRenderer.flipX = !(touchPos.x > transform.position.x);

        int dir = touchPos.x > transform.position.x ? 1 : -1;
        transform.Translate(dir * p_Base.speed * Time.deltaTime,0,0);
    }

    bool WallControll(Vector2 touchPos) {
        Vector2 dir = touchPos.x < transform.position.x ? Vector2.left : Vector2.right;
        return Fast.LookInDir(transform.position,dir,spriteRenderer.bounds.extents.x +0.1f,p_Base.settings.mask);
    }
}
