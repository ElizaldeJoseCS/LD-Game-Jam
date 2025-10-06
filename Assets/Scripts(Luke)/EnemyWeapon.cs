using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float damage = 1f;
    private bool hasDealtDamage = false;

    private Collider2D hitboxCollider;

    public void ResetDamage()
    {
        hasDealtDamage = false;
    }

    private void Awake()
    {
        hitboxCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        if (!hasDealtDamage)
        {
            CheckForPlayer();
        }
    }

    private void CheckForPlayer()
    {
        // This will store any colliders currently overlapping the hitbox
        Collider2D[] results = new Collider2D[10];
        int count = Physics2D.OverlapCollider(hitboxCollider, new ContactFilter2D().NoFilter(), results);

        for (int i = 0; i < count; i++)
        {
            Collider2D hit = results[i];
            if (hit != null && hit.CompareTag("Player")) // check tag to confirm it's the player
            {
                player player = hit.GetComponent<player>();
                if (player != null)
                {
                    player.TakeDamage(damage);  // call the player's TakeDamage function
                    hasDealtDamage = true; // ensure damage is only applied once per activation
                }
            }
        }
    }
}