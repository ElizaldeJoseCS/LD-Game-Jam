using UnityEngine;

public class Attack : MonoBehaviour
{
    public GameObject Melee;
    bool isAttacking = false;
    float attackDuration = 0.1f;
    float attackTimeRemaining = 0f;

    void Update()
    {
        if (isAttacking)
        {
            attackTimeRemaining -= Time.deltaTime;
            if (attackTimeRemaining <= 0f)
            {
                isAttacking = false;
                Melee.SetActive(false);
                Melee.GetComponent<Weapon>().ResetDamage();
            }
        }
    }

    void OnAttack()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            Melee.SetActive(true);
            attackTimeRemaining = attackDuration;
        }
    }
}
