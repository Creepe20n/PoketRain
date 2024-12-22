using UnityEngine;

public class Fall_Entity : EntityBase
{
    public static float moveTime = 1f;

    [SerializeField] private ParticleSystem preKillParticle;
    private ParticleSystem killParticle;

    [SerializeField] GameObject preOnKillEventObj;
    private EventBase onKillEvent;//The event that gets triggerd when the object hits entity or ground

    [HideInInspector] public GameManager gameManager;

    public void FixedUpdate() {
        transform.Translate(0,moveTime*-moveSpeed*Time.deltaTime,0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.collider.CompareTag("Entity") || collision.collider.CompareTag("Ground")) {
            HitObject(collision.collider.gameObject);
        }
    }
    public void HitObject(GameObject hitObject) {
        //Trigger event
        if(onKillEvent == null) {
            onKillEvent = Instantiate(preOnKillEventObj).GetComponent<EventBase>();
        }
        onKillEvent.gameObject.SetActive(true);
        onKillEvent.OnEventStart(hitObject,gameManager);
        //Particle
        if(killParticle == null) {
            killParticle = Instantiate(preKillParticle);
        }
        killParticle.transform.position = transform.position;
        killParticle.Play();


        gameObject.SetActive(false);
    }

    public override void KillObject() {
        if(onKillEvent != null) 
            onKillEvent.OnEventEnd();

        gameObject.SetActive(false);
    }
}
