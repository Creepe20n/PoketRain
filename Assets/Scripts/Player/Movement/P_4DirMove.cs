using FastThings.FastBase;
using FastThings.FastTouch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_4DirMove : MonoBehaviour
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
        
        transform.position = Vector2.MoveTowards(transform.position,touchPos,p_Base.speed*Time.deltaTime);
    }

    bool WallControll(Vector2 touchPos) {
        Vector2 dir = touchPos.x < transform.position.x ? Vector2.left : Vector2.right;
        return Fast.LookInDir(transform.position,dir,spriteRenderer.bounds.extents.x + 0.1f,p_Base.settings.mask);
    }
}
