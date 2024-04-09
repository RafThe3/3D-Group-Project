using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private float attackDistance = 0.5f, damageDelay = 1;
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private GameObject attackPoint;
    [SerializeField] private AudioClip[] damageSFX;

    //Internal Variables
    private AudioSource audioSource;
    private float timer;
    [HideInInspector] public bool isAttacking;
    private Animator animator;

    public int Song { get; set; } = 0;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        timer = damageDelay;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        isAttacking = Input.GetKeyDown(KeyCode.Mouse0) && timer >= damageDelay;

        if (isAttacking && GetComponent<PlayerMovement>().isGrounded)
        {
            Attack();
        }

    }

    private void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackDistance, LayerMask.GetMask("Enemy"));

        foreach (Collider2D enemy in enemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(damageAmount);
            PlaySFX(damageSFX);
        }
        animator.SetTrigger("Attack");
        timer = 0;
    }

    private void PlaySFX(AudioClip[] audioClips)
    {
        if (Song != audioClips.Length)
        {
            PlayNextSFX(audioClips);
        }
    }

    private void PlayNextSFX(AudioClip[] audios)
    {
        if (Song < audios.Length - 1)
        {
            Song++;
        }
        else
        {
            Song = 0;
        }
        audioSource.PlayOneShot(audios[Song]);
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.transform.position, attackDistance);
        }
    }
}
