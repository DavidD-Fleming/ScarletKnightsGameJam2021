using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
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
        if (Input.GetKey(KeyCode.Space))
        {
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
}
