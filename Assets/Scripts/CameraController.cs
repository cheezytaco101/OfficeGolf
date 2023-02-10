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
        this.size = camera.orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        transform.position = new Vector3(Mathf.Lerp(transform.position.x, follow.transform.position.x, lerpAmount), Mathf.Lerp(transform.position.y, follow.transform.position.y, lerpAmount), -10);

        if (Input.GetMouseButton(0)) camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, zoomSize, zoomSpeed);
        else camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, size, zoomSpeed);

    }
}
