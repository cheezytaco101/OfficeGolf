using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject follow;
    public float lerpAmount = 0.5f;

    public float zoomSize = 4f;
    public float zoomSpeed = 0.1f;
    private float size;

    private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        //reset camera size
        this.size = camera.orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Lerp towards player
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, follow.transform.position.x, lerpAmount), Mathf.Lerp(transform.position.y, follow.transform.position.y, lerpAmount), -10);
        //Lerp to zoom when mouse is held
        if (Input.GetMouseButton(0)) camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomSize, zoomSpeed);
        //Lerp out size when mouse released
        else camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, size, zoomSpeed);

    }
}
