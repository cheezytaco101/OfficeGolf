using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairDudeController : MonoBehaviour
{

    public GameObject LevelHandler;

    public GameObject top;
    public GameObject bottom;
    public GameObject shotgun;
    public GameObject shotgunPivot;
    public GameObject shotgunBlast;
    public GameObject shotgunScatter;
    public GameObject shotgunSmoke;
    public GameObject shotgunShell;
    public GameObject noteParticle;

    private Animator topAnimator;
    private Animator bottomAnimator;
    private SpriteRenderer shotgunSprite;

    private AudioSource audioSource;
    public GameObject shotgunClickAudio;
    public GameObject shotgunBlastAudio;
    public GameObject thumpAudio;
    public GameObject rollingAudio;


    private Rigidbody2D body;

    public float shotForce = 10f;

    private float fixedDeltaTime;

    private bool idle = false;


    // Start is called before the first frame update
    void Awake()
    {

        topAnimator = top.GetComponent<Animator>();
        bottomAnimator = bottom.GetComponent<Animator>();
        shotgunSprite = shotgun.GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        this.fixedDeltaTime = Time.fixedDeltaTime;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 relativeMousePosition = new Vector2(mouseWorldPosition.x - transform.position.x, mouseWorldPosition.y - transform.position.y);

        if (relativeMousePosition.x < 0) shotgunSprite.flipY = true;
        if (relativeMousePosition.x > 0) shotgunSprite.flipY = false;

        float shotgunRotation = Mathf.Atan2(relativeMousePosition.y, relativeMousePosition.x) * Mathf.Rad2Deg;
        shotgunPivot.transform.rotation = Quaternion.Euler(0,0,shotgunRotation);

        Shoot(relativeMousePosition);

        topAnimator.SetFloat("MouseX", relativeMousePosition.x);
        topAnimator.SetFloat("MouseY", relativeMousePosition.y);

        bottomAnimator.SetFloat("VelocityX", body.velocity.x);
        bottomAnimator.SetFloat("VelocityY", body.velocity.y);

        Idle();

        thumpAudio.GetComponent<AudioSource>().volume = body.velocity.magnitude / 30;

    }

    void Shoot(Vector2 relativeMousePosition) 
    {

        if (Input.GetMouseButtonDown(0))
        {

            Time.timeScale = 0.5f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            shotgunClickAudio.GetComponent<AudioSource>().Play();

        }

        if (Input.GetMouseButtonUp(0) && LevelHandler.GetComponent<LevelHandler>().ammo != 0)
        {

            Time.timeScale = 1;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            body.velocity = ((relativeMousePosition.normalized * shotForce) * -1);
            shotgunBlast.GetComponent<ParticleSystem>().Play();
            shotgunSmoke.GetComponent<ParticleSystem>().Play();
            shotgunScatter.GetComponent<ParticleSystem>().Play();
            shotgunShell.GetComponent<ParticleSystem>().Play();
            shotgunBlastAudio.GetComponent<AudioSource>().Play();
            LevelHandler.GetComponent<LevelHandler>().ammo -= 1;

        }

    }

    void Idle()
    {

        if (body.velocity.magnitude < 1 && idle != true)
        {
            topAnimator.SetBool("Idle", true);
            noteParticle.GetComponent<ParticleSystem>().Play();
            idle = true;
        }
        else if (body.velocity.magnitude >= 1)
        {
            topAnimator.SetBool("Idle", false);
            noteParticle.GetComponent<ParticleSystem>().Stop();
            idle = false;
            
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        thumpAudio.GetComponent<AudioSource>().Play();
    }
}
