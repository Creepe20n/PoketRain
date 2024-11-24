using UnityEngine;
using PoketAPI.Convert;
[RequireComponent(typeof(PlayerBase))]
public class Player2DMovement : MonoBehaviour
{
    private PlayerBase player;
    private int moveDir = 0;
    private Vector2 touchStartPosition;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start() {
        player = GetComponent<PlayerBase>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    private void Update() {
        if(player.frezePlayer || Input.touchCount <= 0) {
            moveDir = 0;
            animator.Play("Idle");
            return;
        }

        Touch touch = Input.GetTouch(0);
        Vector2 touchPos = ConvertPosition.ToWorldPoint(touch.position);

        if(ConvertPosition.X_Distance(transform.position, touchPos) < 0.05f) {
            moveDir = 0;
            animator.Play("Idle");
            return;
        }

        moveDir = touchPos.x < transform.position.x ? -1 : 1;

        spriteRenderer.flipX = moveDir < 0;

        animator.Play("Run");
    }

    private void FixedUpdate() {
        transform.Translate(moveDir*player.moveSpeed*Time.deltaTime,0,0);
    }
}
