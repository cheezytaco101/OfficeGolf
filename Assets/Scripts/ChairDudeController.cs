using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairDudeController : MonoBehaviour
{

    public GameObject top;
    public GameObject bottom;

    private Animator topAnimator;
    private Animator bottomAnimator;

    private Rigidbody2D body;


    // Start is called before the first frame update
    void Awake()
    {

        topAnimator = top.GetComponent<Animator>();
        bottomAnimator = bottom.GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 relativeMousePosition = new Vector2(mouseWorldPosition.x - transform.position.x, mouseWorldPosition.y - transform.position.y);

        topAnimator.SetFloat("MouseX", relativeMousePosition.x);
        topAnimator.SetFloat("MouseY", relativeMousePosition.y);

        bottomAnimator.SetFloat("VelocityX", body.velocity.x);
        bottomAnimator.SetFloat("VelocityY", body.velocity.y);



    }

    void Shoot() 
    {
        


    }
}
