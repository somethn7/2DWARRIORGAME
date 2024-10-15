using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Transform enemyAttackPoint;
    public LayerMask playerLayers;

    [SerializeField] private CharacterHealth characterHealth;

    public float enemyAttackRange = 0.5f;
    public int enemyAttackDamage = 40;

    public void DamagePlayer()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(enemyAttackPoint.position, enemyAttackRange, playerLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            characterHealth.TakeDamage(enemyAttackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (enemyAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(enemyAttackPoint.position, enemyAttackRange);
    }
}