using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    public Text pauseText;
    public Button resume;
    public Button toMenu;
    private bool paused = false;
    public float baseTimeScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        pauseText.enabled = false;
        resume.gameObject.SetActive(false);
        toMenu.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPause();
    }

    public void CheckForPause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ChangePauseState();
        }
    }

    public void ChangePauseState()
    {
        if(!paused)
        {
            PauseGame();
        }
        else
        {
            UnpauseGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        ChangeStates();
    }

    public void UnpauseGame()
    {
        Time.timeScale = baseTimeScale;
        ChangeStates();
    }

    private void ChangeStates()
    {
        paused = !paused;
        AudioListener.pause = paused;
        pauseText.enabled = paused;
        resume.gameObject.SetActive(paused);
        toMenu.gameObject.SetActive(paused);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
