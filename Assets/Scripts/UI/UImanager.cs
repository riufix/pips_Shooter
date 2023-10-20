using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    [SerializeField, Scene] string _playScene;
    [SerializeField] GameObject _pauseMenu;
    [SerializeField] GameObject _gameOverMenu;
    [SerializeField] bool _canBeActive = false;

    /*public static UImanager Instance;

    public bool CanBeActive { get => _canBeActive; }

    private void Awake()
    {
        Instance = this;
    }*/

    void Start()
    {
        _pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_canBeActive)
        {
            _pauseMenu.SetActive(true);
            _canBeActive = false;
        }

        if(PlayerLife.Instance.IsDead)
        {
            _gameOverMenu.SetActive(true);
            _canBeActive = false;
            Time.timeScale = 0f;
        }
    }

    public void play()
    {
        SceneManager.LoadScene(_playScene);
    }

    public void resume()
    {
        _pauseMenu.SetActive(false);
        _canBeActive = true;
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void quitToMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void quit()
    {
        Application.Quit();
    }
}
