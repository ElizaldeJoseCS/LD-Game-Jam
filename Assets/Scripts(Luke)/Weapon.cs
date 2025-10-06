using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1f;
    private bool hasDealtDamage = false;

    public void ResetDamage()
    {
        hasDealtDamage = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !hasDealtDamage)
        {
            // Apply damage to the enemy
            AIChase enemy = collision.GetComponent<AIChase>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                hasDealtDamage = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !hasDealtDamage)
        {
            // Apply damage to the enemy
            AIChase enemy = collision.GetComponent<AIChase>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                hasDealtDamage = true;
            }
        }
    }
}
