using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundWave : MonoBehaviour
{
    // public variables
    public float speed;

    // public game objects
    public LayerMask playerLayer;
    public LayerMask enemyLayer;
    public LayerMask jellyLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // constant movement to the right
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the collided gameobject's mask is the player mask, ends game if it is
        if ((playerLayer.value & 1 << other.gameObject.layer) != 0)
        {
            GameObject.Find("Game").GetComponent<GameOver>().OnGameOver();
        }

        // checks if the collided gameobject's mask is the enemy mask, destroys enemy if it is (rn just makes them slide through the ground which i kinda like better?)
        if ((enemyLayer.value & 1 << other.gameObject.layer) != 0)
        {
            Destroy(other);
        }

        // checks if the collided gameobject's mask is the jelly mask, changes direction if it is
        if ((jellyLayer.value & 1 << other.gameObject.layer) != 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f) * Quaternion.Inverse(transform.rotation);
        }
        else
        {
            // destroy sound wave once it collides with something
            Destroy(gameObject);
        }
    }
}
