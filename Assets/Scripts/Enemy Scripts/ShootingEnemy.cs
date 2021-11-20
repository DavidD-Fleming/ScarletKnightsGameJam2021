using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    // public game objects
    public LayerMask playerLayer;
    public GameObject enemySoundWavePrefab;

    // public variables
    public float cooldown;
    public float rayDistance;

    // private variables
    SpriteRenderer sprite;
    Vector2 aimAtVector;
    Vector3 soundWaveStartAdjustment;
    float nextCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        // get components
        sprite = gameObject.GetComponent<SpriteRenderer>();

        // checks what direction the sprite is facing, and adjusts for that
        if (sprite.flipX)
        {
            aimAtVector = Vector2.left;
            soundWaveStartAdjustment = new Vector3(-2f, 0f, 0f);
        }
        else
        {
            aimAtVector = Vector2.right;
            soundWaveStartAdjustment = new Vector3(2f, 0f, 0f);
        }
    }

    // Update is fixed
    void FixedUpdate()
    {
        // sets raycast to check for players
        RaycastHit2D playerRaycast = Physics2D.Raycast(transform.position, aimAtVector, rayDistance, playerLayer);

        // shoots sound wave if cooldown is up and detects player
        if (playerRaycast.collider != null && nextCooldown < Time.time)
        {
            GameObject newSoundWave = (GameObject)Instantiate(enemySoundWavePrefab, transform.position + soundWaveStartAdjustment, Quaternion.identity);
            nextCooldown = Time.time + cooldown;
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
