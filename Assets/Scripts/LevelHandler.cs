using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{

    public int ammo;
    public string nextLevel;
    public string currentLevel;

    public GameObject coffee;
    public GameObject restartNotif;

    private void Update()
    {

        if (coffee == null)
        {
            Invoke("LoadNext", 1);
        }
        else if (ammo == 0 && coffee != null)
        {
            Invoke("RestartNotif", 10);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
                
    }

    private void LoadNext()
    {
        SceneManager.LoadScene(sceneName: nextLevel);
    }

    private void Restart()
    {
        SceneManager.LoadScene(sceneName: currentLevel);
    }

    private void RestartNotif()
    {
        restartNotif.SetActive(true);
    }

    private void LoadMenu()
    {

    }

}
