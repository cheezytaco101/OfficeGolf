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

    public float bulletTime = 0.5f;


    // Start is called before the first frame update
    void Awake()
    {

        //Get Components
        topAnimator = top.GetComponent<Animator>();
        bottomAnimator = bottom.GetComponent<Animator>();
        shotgunSprite = shotgun.GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        //Reset time scale to default values
        Time.fixedDeltaTime = 0.02f;
        this.fixedDeltaTime = 0.02f;
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {

        //Get world space mouse position
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 relativeMousePosition = new Vector2(mouseWorldPosition.x - transform.position.x, mouseWorldPosition.y - transform.position.y);

        //Flip shotgun sprite depending on which side of the player it's on
        if (relativeMousePosition.x < 0) shotgunSprite.flipY = true;
        if (relativeMousePosition.x > 0) shotgunSprite.flipY = false;

        //Rotate the shotgun around the player
        float shotgunRotation = Mathf.Atan2(relativeMousePosition.y, relativeMousePosition.x) * Mathf.Rad2Deg;
        shotgunPivot.transform.rotation = Quaternion.Euler(0,0,shotgunRotation);

        //Run shoot function
        Shoot(relativeMousePosition);

        //Set frame for top sprite rotation
        topAnimator.SetFloat("MouseX", relativeMousePosition.x);
        topAnimator.SetFloat("MouseY", relativeMousePosition.y);

        //Set frame for bottom sprite direction
        bottomAnimator.SetFloat("VelocityX", body.velocity.x);
        bottomAnimator.SetFloat("VelocityY", body.velocity.y);

        //Run idle function
        Idle();

        //Adjust volume of bump relative to velocity
        thumpAudio.GetComponent<AudioSource>().volume = body.velocity.magnitude / 60;

    }

    //Handles the player shooting ability
    void Shoot(Vector2 relativeMousePosition) 
    {
        //On mouse hold
        if (Input.GetMouseButtonDown(0))
        {

            //Initiate bullet time
            Time.timeScale = bulletTime;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            //Play click sound effect
            shotgunClickAudio.GetComponent<AudioSource>().Play();

        }

        //On mouse release
        if (Input.GetMouseButtonUp(0) && LevelHandler.GetComponent<LevelHandler>().ammo != 0)
        {

            //Reset normal time
            Time.timeScale = 1;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            //Add velocity to body
            body.velocity = ((relativeMousePosition.normalized * shotForce) * -1);
            //Play particle effects and audio effects
            shotgunBlast.GetComponent<ParticleSystem>().Play();
            shotgunSmoke.GetComponent<ParticleSystem>().Play();
            shotgunScatter.GetComponent<ParticleSystem>().Play();
            shotgunShell.GetComponent<ParticleSystem>().Play();
            shotgunBlastAudio.GetComponent<AudioSource>().Play();
            //Update ammo count and ui
            LevelHandler.GetComponent<LevelHandler>().ammo -= 1;
            LevelHandler.GetComponent<LevelHandler>().UpdateShells();
        }

    }

    //Idle animation
    void Idle()
    {

        //Detect if body is near motionless, and begin idle animationa
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

    //Play thump each time player collides with an object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        thumpAudio.GetComponent<AudioSource>().Play();
    }
}
