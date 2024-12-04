using UnityEngine;

public class Fall_Entity : EntityBase
{
    public static float moveTime = 1f;

    [SerializeField] private ParticleSystem preKillParticle;
    private ParticleSystem killParticle;

    [SerializeField] GameObject preOnKillEventObj;
    private EventBase onKillEvent;

    public void FixedUpdate() {
        transform.Translate(0,moveTime*moveSpeed*Time.deltaTime,0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("Player")) {
            KillObject();
        }
        if(collision.collider.CompareTag("Respawn")) {
            KillObject();
        }
    }
    public override void KillObject() {

        if(onKillEvent == null) {
            onKillEvent = Instantiate(preOnKillEventObj).GetComponent<EventBase>();
        }
        onKillEvent.OnEventStart();

        if(killParticle == null) {
            killParticle = Instantiate(preKillParticle);
        }
        killParticle.transform.position = transform.position;
        killParticle.Play();


        gameObject.SetActive(false);
    }
}
