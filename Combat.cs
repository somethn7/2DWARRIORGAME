using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public LayerMask skeletonLayers;
    public LayerMask ghostLayers;
    public LayerMask angelLayers;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;

    public Skeleton skeleton;
    public Angel angel;

    public void DamageEnemy()
    {
        // Enemy'leri tespit et
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Debug log ile hasar verilen enemy'yi kontrol et
            Debug.Log("Damage to: " + enemy.name);

            // EnemyMove bileşenini al ve hasar ver
            EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
            if (enemyMove != null)
            {
                enemyMove.TakeDamageEnemy(attackDamage);
            }
        }
    }

    public void DamageGhosts()
    {
        // Ghost'ları tespit et
        Collider2D[] hitGhosts = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, ghostLayers);

        foreach (Collider2D ghostCollider in hitGhosts)
        {
            Ghost ghost = ghostCollider.GetComponent<Ghost>();

            if (ghost != null)
            {
                Debug.Log("Damaging Ghost: " + ghost.name);
                ghost.TakeDamageGhost(attackDamage);
                Debug.Log("Ghost hasar aldı");
            }
        }
    }

    public void DamageSkeleton()
    {

        Collider2D[] hitSkeletons = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, skeletonLayers);

        skeleton.TakeDamageSkeleton(attackDamage);
        StartCoroutine(Waitasecond());


        /*foreach (Collider2D skeletonCollider in hitSkeletons)
        {
            Skeleton skeleton = skeletonCollider.GetComponent<Skeleton>();
            if (skeleton != null)
            {
                Debug.Log("Damaging Skeleton: " + skeleton.name);
                skeleton.TakeDamageSkeleton(attackDamage);
                Debug.Log("Skeleton hasar aldı");
            }
       */
    }

    public void DamageAngel()
    {
        Collider2D[] hitAngels = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, angelLayers);
        angel.TakeDamageAngel(attackDamage);
        StartCoroutine(Waittwosecond());

    }

    IEnumerator Waitasecond()
    {
        yield return new WaitForSeconds(1f);
        skeleton.Die();
    }

    IEnumerator Waittwosecond()
    {
        yield return new WaitForSeconds(1f);
        angel.Die();
    }
}
    



