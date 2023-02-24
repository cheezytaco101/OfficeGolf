using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{

    private void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.LoadScene(sceneName: "MainMenu");
    }

}
