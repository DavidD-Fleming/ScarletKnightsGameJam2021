using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBasicEnemy : MonoBehaviour
{
    // public game objects
    public LayerMask playerLayer;
    // DESTINATIONONE IS ALWAYS RIGHT OF DESTINATION TWO
    public Transform destinationOne;
    public Transform destinationTwo;

    // public variables
    public float speed;

    // private variables
    bool lastAtDestinationOne;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        // get components
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastAtDestinationOne)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationTwo.position, speed * Time.deltaTime);
            sprite.flipX = true;
        }
        if (lastAtDestinationOne == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationOne.position, speed * Time.deltaTime);
            sprite.flipX = false;
        }
        if (Vector3.Distance(transform.position, destinationOne.position) < 0.001f)
        {
            lastAtDestinationOne = true;
        }
        if (Vector3.Distance(transform.position, destinationTwo.position) < 0.001f)
        {
            lastAtDestinationOne = false;
        }
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
