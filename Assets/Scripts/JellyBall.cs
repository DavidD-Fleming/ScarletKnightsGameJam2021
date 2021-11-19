using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyBall : MonoBehaviour
{
    // public variables
    public float speed;

    // public game objects
    public LayerMask platformLayer;
    public GameObject jellyBlockPrefab;

    // Update is called once per frame
    void FixedUpdate()
    {
        // constant movement to the right
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the collided gameobject's mask is the player mask, adds force if it is
        if ((platformLayer.value & 1 << other.gameObject.layer) != 0)
        {
            GameObject newjellyBlock = (GameObject)Instantiate(jellyBlockPrefab, transform.position, Quaternion.identity);
        }

        // destroy sound wave once it collides with something
        Destroy(gameObject);
    }
}
