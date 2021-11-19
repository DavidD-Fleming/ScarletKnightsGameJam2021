using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    // public game objects
    [Header("Game Objects")]
    public Transform groundCheckObject;
    public LayerMask platformLayer;
    public LayerMask jellyLayer;
    public GameObject soundWavePrefab;
    public GameObject soundWaveAim;
    public GameObject soundWaveStart;
    public GameObject soundWaveAimL;
    public GameObject soundWaveStartL;
    public GameObject soundWaveCooldownIndicator;

    // public variables
    [Header("Base Variables")]
    public float forceX;
    public float maxVelocityX;
    public float forceY;
    public float jellyForceY;
    public float adjustAimSpeed;
    public float cooldown;

    // private game objects
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    // private variables
    bool isGrounded;
    float nextCooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        // get components
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is frequent
    void Update()
    {
        // sound wave (bad code but tired)
        if (nextCooldown < Time.time)
        {
            soundWaveCooldownIndicator.SetActive(false);
            Vector3 soundWaveInitialAim = new Vector3(soundWaveAim.transform.position.x, soundWaveAim.transform.position.y, 0f);
            Vector3 soundWaveInitialAimL = new Vector3(soundWaveAimL.transform.position.x, soundWaveAimL.transform.position.y, 0f);
            if (sprite.flipX)
            {
                if (Input.GetKey(KeyCode.F))
                {
                    float soundWaveChangedAimY = soundWaveInitialAimL.y;
                    if (Input.GetKey(KeyCode.W))
                    {
                        soundWaveChangedAimY += adjustAimSpeed * Time.deltaTime;
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        soundWaveChangedAimY -= adjustAimSpeed * Time.deltaTime;
                    }
                    //soundWaveChangedAimY = Mathf.Clamp(soundWaveChangedAimY, 0f, 3f);
                    soundWaveAimL.transform.position = new Vector3(soundWaveAimL.transform.position.x, soundWaveChangedAimY, 0f);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    soundWaveAimL.transform.position = soundWaveStartL.transform.position - new Vector3(1.5f, 0f, 0f);
                    soundWaveAimL.SetActive(true);
                }
                if (Input.GetKeyUp(KeyCode.F))
                {
                    Vector3 vectorToTarget = soundWaveAimL.transform.position - soundWaveStartL.transform.position;
                    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    GameObject newSoundWave = (GameObject)Instantiate(soundWavePrefab, soundWaveStartL.transform.position, q);
                    soundWaveAimL.SetActive(false);
                    nextCooldown = Time.time + cooldown;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.F))
                {
                    float soundWaveChangedAimY = soundWaveInitialAim.y;
                    if (Input.GetKey(KeyCode.W))
                    {
                        soundWaveChangedAimY += adjustAimSpeed * Time.deltaTime;
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        soundWaveChangedAimY -= adjustAimSpeed * Time.deltaTime;
                    }
                    //soundWaveChangedAimY = Mathf.Clamp(soundWaveChangedAimY, 0f, 3f);
                    soundWaveAim.transform.position = new Vector3(soundWaveAim.transform.position.x, soundWaveChangedAimY, 0f);
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    soundWaveAim.transform.position = soundWaveStart.transform.position + new Vector3(1.5f, 0f, 0f);
                    soundWaveAim.SetActive(true);
                }
                if (Input.GetKeyUp(KeyCode.F))
                {
                    Vector3 vectorToTarget = soundWaveAim.transform.position - soundWaveStart.transform.position;
                    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    GameObject newSoundWave = (GameObject)Instantiate(soundWavePrefab, soundWaveStart.transform.position, q);
                    soundWaveAim.SetActive(false);
                    nextCooldown = Time.time + cooldown;
                }
            }
        } else
        {
            soundWaveCooldownIndicator.SetActive(true);
        }
    }

    // Update is fixed
    void FixedUpdate()
    {
        // sets raycast to check if player is grounded
        RaycastHit2D groundRaycast = Physics2D.Raycast(groundCheckObject.position, Vector2.down, 0.25f, platformLayer);
        RaycastHit2D jellyRaycast = Physics2D.Raycast(groundCheckObject.position, Vector2.down, 0.25f, jellyLayer);

        // horizontal movement
        float inputX = 0;
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.F))
        {
            inputX -= 1;
            sprite.flipX = true;
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.F))
        {
            inputX += 1;
            sprite.flipX = false;
        }
        if (rb.velocity.magnitude < maxVelocityX)
        {
            Vector2 movementX = new Vector2(inputX, 0);
            rb.AddForce(forceX * movementX);
        }

        // vertical movement if grounded and not aiming
        if (Input.GetKey(KeyCode.W) && groundRaycast.collider != null && !Input.GetKey(KeyCode.F))
        {
            Vector2 movementY = new Vector2(0, forceY);
            rb.AddForce(movementY);
        }

        if (Input.GetKey(KeyCode.W) && jellyRaycast.collider != null && !Input.GetKey(KeyCode.F))
        {
            Vector2 movementY = new Vector2(0, jellyForceY);
            rb.AddForce(movementY);
        }
    }
}
