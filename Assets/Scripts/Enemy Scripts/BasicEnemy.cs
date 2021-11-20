using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    // public game objects
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // checks if the collided gameobject's mask is the player mask, ends game if it is
        if ((playerLayer.value & 1 << collision.gameObject.layer) != 0)
        {
            GameObject.Find("Game").GetComponent<GameOver>().OnGameOver();
        }
    }
}
