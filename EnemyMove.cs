using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Animator anim;
    public int maxHealth = 80;
    private int currentHealth;
    public GameObject wizard;

    [SerializeField] private AIEnemy enemyai;
    
    [SerializeField] private AudioManage audioManager;
    [SerializeField] private string enemySound;
    
    
    
    

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamageEnemy(int damage)
    {
        currentHealth -= damage;
        
        anim.SetTrigger("Hurt");
        
        if (audioManager != null)
        {
            audioManager.Play(enemySound);
        }


        if (currentHealth <=0)
        {
            Die();
        }
    }

    void Die()
    {
        anim.SetTrigger("isDead");

        enemyai.enabled = false;
        this.enabled = false;
        enemyai.followspeed = 0;
    }

    private void OnDeathAnimationFinished()
    {
        wizard.SetActive(false);
    }
}


