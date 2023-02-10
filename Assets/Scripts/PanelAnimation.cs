using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAnimation : MonoBehaviour
{

    public float focusHeight = 100;
    private RectTransform rectTransform;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetMouseButton(0)) rectTransform.rect.Set();

    }
}
