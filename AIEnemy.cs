using System.Collections;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    public Vector2 pos1;
    public Vector2 pos2;
    public float leftrightspeed;
    private float oldPosition;

    public float distance;
    public float followspeed;

    [SerializeField] private Animator anim;
    [SerializeField] private EnemyCombat enemycombat;
    [SerializeField] private Transform target;
    [SerializeField] private LayerMask layerMask;

    public float startDelay = 1f; 

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        
        StartCoroutine(WaitAndStartAI(startDelay));
    }

    private IEnumerator WaitAndStartAI(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        while (true) 
        {
            EnemyAi();
            yield return null; 
        } 
    }

    private void EnemyMove()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * leftrightspeed, 1.0f));

        if (transform.position.x > oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (transform.position.x < oldPosition)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        oldPosition = transform.position.x;
    }

    private void EnemyAi()
    {
        RaycastHit2D hitEnemy = Physics2D.Raycast(transform.position, -transform.right, distance, layerMask);

        if (hitEnemy.collider != null)
        {
            Debug.DrawLine(transform.position, hitEnemy.point, Color.red);
            anim.SetTrigger("Attack"); 
            anim.SetTrigger("isWalk"); 
            EnemyFollow();
            enemycombat.DamagePlayer();
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position - transform.right * distance, Color.green);
            anim.SetTrigger("isWalk"); 
            EnemyMove();
        }
    }

    private void EnemyFollow()
    {
        Vector3 targetPosition = new Vector3(target.position.x, gameObject.transform.position.y, target.position.x);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followspeed * Time.deltaTime);
    }
}