using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    // public variables
    public float speed;
    public float pushingForce; //500

    // public game objects
    public LayerMask playerLayer;
    public LayerMask jellyLayer;
    public Collider2D soundWaveCollider;

    // Start is called before the first frame update
    void Start()
    {
        soundWaveCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // constant movement to the right
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // checks if the collided gameobject's mask is the player mask, adds force if it is
        if ((playerLayer.value & 1 << other.gameObject.layer) != 0)
        {
            other.attachedRigidbody.AddForce(transform.right * pushingForce);
        }

        // checks if the collided gameobject's mask is the jelly mask, changes direction if it is
        if ((jellyLayer.value & 1 << other.gameObject.layer) != 0)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 180f) * Quaternion.Inverse(transform.rotation);
        } else
        {
            // destroy sound wave once it collides with something
            Destroy(gameObject);
        }
    }
}
