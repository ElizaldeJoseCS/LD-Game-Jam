using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    float movementX;
    float movementY;
    [SerializeField] float speed = 6;
    public float dashSpeed = 15f;
    public float dashDuration = 0.2f;
    public float dashCooldownDuration = 1f;
    private bool dashCooldownStart = false;
    public float health = 3.0f;
    private bool knockback = false;
    public float knockbackDuration = 0.2f;
    public float knockbackForce = 6f;
    private Vector2 direction;

    [SerializeField] GameObject enemy;


    [SerializeField] Rigidbody2D rb;

    public Transform Aim;

    Vector2 lastDirection = Vector2.zero;

    private bool isDashing = false;
    private float dashTimeRemaining = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing && !dashCooldownStart)
        {
            rb.linearVelocity = lastDirection * -dashSpeed;
            dashTimeRemaining -= Time.deltaTime;

            if (dashTimeRemaining <= 0)
            {
                isDashing = false;
                dashCooldownStart = true;
                rb.linearVelocity = Vector2.zero; // stop movement after dash
            }
        }
        else if (dashCooldownStart)
        {
            dashCooldownDuration -= Time.deltaTime;
            if (dashCooldownDuration <= 0)
            {
                dashCooldownStart = false;
                dashCooldownDuration = 1f; // reset cooldown
            }
        }
    }

    void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();

        movementX = v.x;
        movementY = v.y;
    }

    void OnJump(InputValue value)
    {
        Debug.Log(health);
        // can't already be dashing and must be moving to dash
        if (!isDashing)
        {
            isDashing = true;
            dashTimeRemaining = dashDuration;
        }
    }

    void FixedUpdate()
    {
        float XmoveDistance = movementX * speed * Time.fixedDeltaTime;
        float YmoveDistance = movementY * speed * Time.fixedDeltaTime;

        if (!knockback)
        {
            transform.position = new Vector2(transform.position.x + XmoveDistance, transform.position.y + YmoveDistance);
        }
        else 
        {
            knockbackDuration -= Time.deltaTime;
            direction = transform.position - enemy.transform.position;
            direction.Normalize();
            transform.position = new Vector2(transform.position.x + direction.x * knockbackForce * Time.deltaTime, transform.position.y + direction.y * knockbackForce * Time.deltaTime);
            if (knockbackDuration <= 0f)
            { 
                knockback = false;
                rb.linearVelocity = Vector2.zero;
                knockbackDuration = 0.2f;
            }
        }

            Vector3 vector3 = Vector3.left * movementX + Vector3.down * movementY;

        if (movementX == 0 && movementY == 0)
        {
            vector3 = new Vector3(lastDirection.x, lastDirection.y, 0);
        }
        else if (Mathf.Abs(movementX) > Mathf.Abs(movementY))
        {
            movementY = 0;
            if (movementX > 0)
            {
                movementX = 1;
            }
            else
            {
                movementX = -1;
            }
        }
        else if (Mathf.Abs(movementX) <= Mathf.Abs(movementY))
        {
            movementX = 0;
            if (movementY > 0)
            {
                movementY = 1;
            }
            else
            {
                movementY = -1;
            }
        }

        lastDirection = new Vector2(vector3.x, vector3.y);
        Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        knockback = true;
    }
}