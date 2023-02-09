using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairDudeController : MonoBehaviour
{

    public GameObject top;
    public GameObject bottom;
    public GameObject shotgun;
    public GameObject shotgunPivot;

    private Animator topAnimator;
    private Animator bottomAnimator;
    private SpriteRenderer shotgunSprite;

    private Rigidbody2D body;

    public float shotForce = 10f;


    // Start is called before the first frame update
    void Awake()
    {

        topAnimator = top.GetComponent<Animator>();
        bottomAnimator = bottom.GetComponent<Animator>();
        shotgunSprite = shotgun.GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();

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



    }

    void Shoot(Vector2 relativeMousePosition) 
    {

        if (Input.GetMouseButtonDown(0))
        {

            body.AddForce((relativeMousePosition.normalized * shotForce) * -1);

        }

    }
}
