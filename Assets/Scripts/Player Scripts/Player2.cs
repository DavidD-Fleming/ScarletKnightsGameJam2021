using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2 : MonoBehaviour
{
    // public game objects
    [Header("Game Objects")]
    public Transform groundCheckObject;
    public LayerMask platformLayer;
    public LayerMask jellyLayer;
    public GameObject jellyPrefab;
    public GameObject jellyAim;
    public GameObject jellyStart;
    public GameObject jellyAimL;
    public GameObject jellyStartL;
    public Text jellyCountText;

    // public variables
    [Header("Base Variables")]
    public float forceX;
    public float maxVelocityX;
    public float forceY;
    public float jellyForceY;
    public float adjustAimSpeed;
    public float jellyCharge;

    // private game objects
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    // private variables
    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        // get components
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();

        // sets jelly charge
        jellyCountText.text = "Jellies Left: " + jellyCharge;
    }

    // Update is frequent
    void Update()
    {
        // jelly (bad code but tired)
        if (jellyCharge > 0)
        {
            Vector3 jellyInitialAim = new Vector3(jellyAim.transform.position.x, jellyAim.transform.position.y, 0f);
            Vector3 jellyInitialAimL = new Vector3(jellyAimL.transform.position.x, jellyAimL.transform.position.y, 0f);
            if (sprite.flipX)
            {
                if (Input.GetKey(KeyCode.Period))
                {
                    float jellyChangedAimY = jellyInitialAimL.y;
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        jellyChangedAimY += adjustAimSpeed * Time.deltaTime;
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        jellyChangedAimY -= adjustAimSpeed * Time.deltaTime;
                    }
                    //jellyChangedAimY = Mathf.Clamp(jellyChangedAimY, 0f, 3f);
                    jellyAimL.transform.position = new Vector3(jellyAimL.transform.position.x, jellyChangedAimY, 0f);
                }
                if (Input.GetKeyDown(KeyCode.Period))
                {
                    jellyAimL.transform.position = jellyStartL.transform.position - new Vector3(1.5f, 0f, 0f);
                    jellyAimL.SetActive(true);
                }
                if (Input.GetKeyUp(KeyCode.Period))
                {
                    Vector3 vectorToTarget = jellyAimL.transform.position - jellyStartL.transform.position;
                    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    GameObject newjelly = (GameObject)Instantiate(jellyPrefab, jellyStartL.transform.position, q);
                    jellyAimL.SetActive(false);
                    jellyCharge--;

                    // updates jelly count
                    jellyCountText.text = "Jellies Left: " + jellyCharge;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.Period))
                {
                    float jellyChangedAimY = jellyInitialAim.y;
                    if (Input.GetKey(KeyCode.UpArrow))
                    {
                        jellyChangedAimY += adjustAimSpeed * Time.deltaTime;
                    }
                    if (Input.GetKey(KeyCode.DownArrow))
                    {
                        jellyChangedAimY -= adjustAimSpeed * Time.deltaTime;
                    }
                    //jellyChangedAimY = Mathf.Clamp(jellyChangedAimY, 0f, 3f);
                    jellyAim.transform.position = new Vector3(jellyAim.transform.position.x, jellyChangedAimY, 0f);
                }
                if (Input.GetKeyDown(KeyCode.Period))
                {
                    jellyAim.transform.position = jellyStart.transform.position + new Vector3(1.5f, 0f, 0f);
                    jellyAim.SetActive(true);
                }
                if (Input.GetKeyUp(KeyCode.Period))
                {
                    Vector3 vectorToTarget = jellyAim.transform.position - jellyStart.transform.position;
                    float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                    GameObject newjelly = (GameObject)Instantiate(jellyPrefab, jellyStart.transform.position, q);
                    jellyAim.SetActive(false);
                    jellyCharge--;

                    // updates jelly count
                    jellyCountText.text = "Jellies Left: " + jellyCharge;
                }
            }
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
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.Period))
        {
            inputX -= 1;
            sprite.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.Period))
        {
            inputX += 1;
            sprite.flipX = false;
        }
        if (rb.velocity.magnitude < maxVelocityX)
        {
            Vector2 movementX = new Vector2(inputX, 0);
            rb.AddForce(forceX * movementX);
        }

        // vertical movement
        if (Input.GetKey(KeyCode.UpArrow) && groundRaycast.collider != null && !Input.GetKey(KeyCode.Period))
        {
            Vector2 movementY = new Vector2(0, forceY);
            rb.AddForce(movementY);
        }
        if (Input.GetKey(KeyCode.UpArrow) && jellyRaycast.collider != null && !Input.GetKey(KeyCode.Period))
        {
            Vector2 movementY = new Vector2(0, jellyForceY);
            rb.AddForce(movementY);
        }
    }
}
