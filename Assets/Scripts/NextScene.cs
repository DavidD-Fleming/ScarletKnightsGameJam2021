using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    // public game objects
    public int levelNumberToLoad;
    public string levelStringToLoad;

    // public variables
    public bool useIntegerToLoad = false;

    // private variables
    int goNextWhenTwo = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goNextWhenTwo == 2)
        {
            LoadScene();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObject = collision.gameObject;
        if (collisionObject.name == "Player One")
        {
            goNextWhenTwo++;
        }
        if (collisionObject.name == "Player Two")
        {
            goNextWhenTwo++;
        }
    }

    void LoadScene()
    {
        if (useIntegerToLoad)
        {
            SceneManager.LoadScene(levelNumberToLoad);
        } else
        {
            SceneManager.LoadScene(levelStringToLoad);
        }
    }
}
