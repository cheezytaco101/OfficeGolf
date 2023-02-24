using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsHandler : MonoBehaviour
{

    //Used as a breif tutorial message at the beggining of the game
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(this.gameObject);
        }
    }
}
