using UnityEngine;
using UnityEngine.SceneManagement;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float chaseRange = 5.0f;

    private float distance;
    public bool touching = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
<<<<<<< Updated upstream
=======
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
        Debug.Log("Enemy Health: " + health);
>>>>>>> Stashed changes
    }

    void Die()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Act 3");
    }
}
