using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject endMenu;
    public GameObject winMenu;
    
    void showStartMenu()
    {
        Time.timeScale = 0;
        timer.timeRemaining = 0;
        startMenu.SetActive(true);
    }

    void closeStartMenu()
    {
        startMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void showEndgame()
    {
        Time.timeScale = 0;
        endMenu.SetActive(true);
    }

    void closeEndgame()
    {
        endMenu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    void showWinScreen()
    {
        Time.timeScale = 0;
        winMenu.SetActive(true);
    }

    void closeWinScreen()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    private Timer timer;
    private Player player;
    private GameObject home;

    private void Awake()
    {
        timer = FindObjectOfType<Timer>();
        player = FindObjectOfType<Player>();
        home = FindObjectOfType<Home>().gameObject;
        showStartMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (startMenu.activeSelf)
            {
                closeStartMenu();
            }
            else if (endMenu.activeSelf)
            {
                closeEndgame();
            }
            else if (winMenu.activeSelf)
            {
                closeWinScreen();
            }
            else
            {
                showStartMenu();
            }
        }

        if(player.GetComponent<HealthSystem>().health<=0 || home.GetComponent<HealthSystem>().health<=0)
        {
            showEndgame();
        }

        if(timer.timeRemaining >= timer.timerDuration)
        {
            showWinScreen();
        }

    }
}
