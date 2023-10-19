using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;

    //add main menu

    void Start()
    {
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(true);
        }
    }

    public void play()
    {
        SceneManager.LoadScene("");
    }

    public void resume()
    {
        _pauseMenu.SetActive(false);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void quit()
    {
        //Application.Quit();
        //SceneManager.LoadScene("mainMenu");
    }
}
