using UnityEngine;
using UnityEngine.SceneManagement;

public class AIChase : MonoBehaviour
{
    [SerializeField] GameObject Indicator;
    [SerializeField] GameObject Melee;
    public GameObject player;
    [SerializeField] Rigidbody2D rb;
    public float speed;
    public float chaseRange = 15.0f;
    public float attackRange = 3.0f;
    public float attackBuildup = 0.5f;
    public float attackDuration = 0.2f;
    public float attackCooldownTimer = 1.0f;
    private float attackTime;
    private bool windingUp = false;
    public float minDistAway = 2.5f;

    private float distance;
    public bool touching = false;
    private bool attacking = false;
    private bool attackOnCooldown = false;

    public Transform Aim;

    public float health = 15f;
    private bool knockback = false;
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.2f;
    private float knockbackTimeRemaining = 0f;
    public float knockbackCooldownDuration = 0.5f;
    private bool knockbackCooldownStart = false;
    private Vector2 direction = Vector2.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        knockbackTimeRemaining = knockbackDuration;
    }

    // Update is called once per frame
    // Update is called once per frame
    void Update()
    {
        if (player != null)
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

            // if we are (in range or are already attacking) and not on cooldown
            if ((distance < attackRange || attacking) && !attackOnCooldown)
            {
                attacking = true;
                if (attackTime <= attackDuration)
                {
                    Indicator.SetActive(false);
                    if (attackTime <= 0)
                    {
                        Melee.SetActive(false);
                        attacking = false;
                        attackOnCooldown = true;
                        attackTime = attackBuildup + attackDuration;
                        Melee.GetComponent<EnemyWeapon>().ResetDamage();
                    }
                    else
                    {
                        Melee.SetActive(true);
                        attackTime -= Time.deltaTime;
                    }
                }
                else
                {
                    attackTime -= Time.deltaTime;
                    Indicator.SetActive(true);
                }
            }
            else if (attackOnCooldown)
            {
                attackCooldownTimer -= Time.deltaTime;
                if (attackCooldownTimer <= 0f)
                {
                    attackOnCooldown = false;
                    attackCooldownTimer = 1.0f; // reset cooldown
                }
            }

            if (distance < chaseRange && distance > minDistAway && !attacking)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }

            direction = player.transform.position - transform.position;
            direction = new Vector2(-direction.x, -direction.y);

            if (!attacking)
            {
                Aim.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            }
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
            Die();
        }
        knockback = true;
    }
    void Die()
    {
        Destroy(gameObject);
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Act 1")
        {
            

            SceneManager.LoadScene("Act 2");
        }
        if (currentScene.name == "Act 2")
        {
            
            SceneManager.LoadScene("Act 3");
        }
         if (currentScene.name == "Act 3")
        {
         
            SceneManager.LoadScene("Final");
        }


    }

}
