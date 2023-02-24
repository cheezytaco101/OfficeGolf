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

        //If the coffee has been destroyed, load the next level and display the win text
        if (coffee == null)
        {
            Invoke("LoadNext", 3);
            winNotif.SetActive(true);
        }
        //If out of ammo and coffee hasn't been taken, display the restart notif after 7 seconds of being idle
        else if (ammo == 0 && coffee != null)
        {
            Invoke("RestartNotif", 7);
        }

        //Restart on button press r
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
                
    }

    //Loads next level
    private void LoadNext()
    {
        SceneManager.LoadScene(sceneName: nextLevel);
    }

    //Restarts level
    private void Restart()
    {
        SceneManager.LoadScene(sceneName: currentLevel);
    }

    //Displays restart notification for player, to inform them of the 'r' button mapping
    private void RestartNotif()
    {
        restartNotif.SetActive(true);
    }

    //Updates the remaining ammo ui
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

}
