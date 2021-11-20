using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    // pause screen
    public GameObject pauseScreen;
    bool isGamePaused = false;

    public int levelNumberToLoad;
    public string levelStringToLoad;
    public bool useIntegerToLoad = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Time.timeScale = 1;
                isGamePaused = false;
                pauseScreen.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                Time.timeScale = 1;
                if (useIntegerToLoad)
                {
                    SceneManager.LoadScene(levelNumberToLoad);
                }
                else
                {
                    SceneManager.LoadScene(levelStringToLoad);
                }
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Time.timeScale = 0;
                isGamePaused = true;
                pauseScreen.SetActive(true);
            }
        }
    }
}
