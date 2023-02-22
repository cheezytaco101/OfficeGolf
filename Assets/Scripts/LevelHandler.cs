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
    public GameObject winNotif;
    public GameObject shellsUi;

    private void Start()
    {
        UpdateShells();
    }

    private void Update()
    {

        if (coffee == null)
        {
            Invoke("LoadNext", 3);
            winNotif.SetActive(true);
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

    public void UpdateShells()
    { 
        for (int i = 0; shellsUi.transform.childCount > i; i++)
        {
            if (i + 1 <= ammo)
            {
                shellsUi.transform.GetChild(i).gameObject.SetActive(true);
            } else
            {
                shellsUi.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void LoadMenu()
    {

    }

}
