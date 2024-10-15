 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar health;

    public bool enemyattack;
    public float enemytimer;

    private bool dead;

    [SerializeField] private EnemyMove enemymove;
    [SerializeField] private EnemyCombat enemyCombat;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private AudioManage audioManager;
    [SerializeField] private string hurtsoundchr;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        currentHealth = maxHealth;
        enemytimer = 1.5f;
        anim = GetComponent<Animator>();
    }

    void EnemyAttackSpacing()
    {
        if (!enemyattack)
        {
            enemytimer -= Time.deltaTime;
        }

        if (enemytimer < 0)
        {
            enemytimer = 0f;
        }

        if (enemytimer == 0f)
        {
            enemyattack = true;
            enemytimer = 1.5f;
        }
    }

    void CharacterDamage()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            enemyattack = false;
        }
    }

    public void TakeDamage(int damage)
    {
        if (dead) return;

        if (enemyattack)
        {
            currentHealth -= damage;
            enemyattack = false;
            anim.SetTrigger("isHurt");
            audioManager.Play(hurtsoundchr);
            Debug.Log($"Health: {currentHealth}");

            health.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }
    }

    public void Die()
    {
        anim.SetTrigger("isDead");
        dead = true;

        characterMovement.enabled = false;
        enemyCombat.enabled = false;
        enemymove.enabled = false;

        _animator.SetTrigger("GameOver");
    }


    private void Update()
    {
        EnemyAttackSpacing();
        CharacterDamage();
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Slimer"))
        {
            Debug.Log("Çarpışma: Slimer ile çarpışma gerçekleşti");
            int damage = 10;
            TakeDamage(damage);
        }
    }
}