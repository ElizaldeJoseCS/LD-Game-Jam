using UnityEngine;

public class AIChase : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float chaseRange = 5.0f;

    private float distance;
    public bool touching = false;

    float health, maxHealth = 100f;
    public Transform Aim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < chaseRange && !touching)
        {
            Vector2 direction = player.transform.position - transform.position;

            Vector2 move = Vector2.zero;

            if (Mathf.Abs(direction.x) > 0.05f)   // not yet lined up horizontally
            {
                move.x = Mathf.Sign(direction.x);  // move left or right
            }
            else if (Mathf.Abs(direction.y) > 0.05f)  // now move vertically
            {
                move.y = Mathf.Sign(direction.y);  // move up or down
            }

            transform.position += (Vector3)(move * speed * Time.deltaTime);

            Vector3 vector3 = new Vector3(-move.x, -move.y, 0);
            Aim.rotation = Quaternion.LookRotation(Vector3.forward, vector3);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        touching = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        touching = false;
    }

    void takeDamage(int damage)
    {
        if (health > 0)
        {
            health -= damage;
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
