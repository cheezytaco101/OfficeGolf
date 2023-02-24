using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnimation : MonoBehaviour
{

    public float focusHeight = 100;
    public float lerpIn = 0.05f;
    public float lerpOut = 0.1f;
    private RectTransform rectTransform;
    private Vector2 initPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        initPosition = rectTransform.anchoredPosition;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Lerp panels to squeeze aspect ratio
        if (Input.GetMouseButton(0))
        {

            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, initPosition + new Vector2(0, focusHeight), lerpIn);

        }

        else
        {

            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, initPosition, lerpOut);

        }
    }
}
