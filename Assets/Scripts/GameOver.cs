using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // game over screen
    public GameObject gameOverScreen;
    bool isGameOver;

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
        if (isGameOver && Input.GetKey(KeyCode.Space))
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
    }

    public void OnGameOver()
    {
        Time.timeScale = 0;
        isGameOver = true;
        gameOverScreen.SetActive(true);
    }
}
