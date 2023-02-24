using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    //Load scene on button press
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName: sceneName);
    }

}
