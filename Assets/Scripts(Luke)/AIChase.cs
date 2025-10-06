using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Rigidbody2D rb;
    public float speed;
    public float chaseRange = 5.0f;

    private float distance;
    public bool touching = false;

    float health, maxHealth = 15f;
    private bool knockback = false;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;
    private float knockbackTimeRemaining = 0f;
    public float knockbackCooldownDuration = 0.5f;
    private bool knockbackCooldownStart = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
        knockbackTimeRemaining = knockbackDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (knockback)
        {
            knockbackTimeRemaining -= Time.deltaTime;
            if (knockbackTimeRemaining <= 0f)
            {
                knockback = false;
                knockbackCooldownStart = true;
                rb.linearVelocity = Vector2.zero;
            }
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, -knockbackForce * Time.deltaTime);
            return; // Skip the rest of the update while in knockback
        }
        else if (knockbackCooldownStart)
        {
            knockbackCooldownDuration -= Time.deltaTime;
            if (knockbackCooldownDuration <= 0f)
            {
                knockbackCooldownStart = false;
                knockbackCooldownDuration = 0.5f; // reset cooldown
                knockbackTimeRemaining = knockbackDuration; // reset knockback duration
            }
        }

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < chaseRange && !touching)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        touching = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        touching = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        knockback = true;
        Debug.Log("Enemy Health: " + health);
    }
}
