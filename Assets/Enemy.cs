using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject goal;
    public bool isChasing=false;
    public GameObject target;
    public float speed = 5.0f;
    public float stunDuration = 1.0f;
    public float bounceForce = 2.0f;
    public float impactStun = 0.3f;
    public float impactBounce = 0.3f;
    public int experience = 20;
    public int damage;
    public float attackCooldown = 1.0f;
    public float attackCountdown = 0f;
    public CapsuleCollider2D attackHitbox;

    private Rigidbody2D rb;
    private float stunTimeRemaining;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (isChasing)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
        
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(attackCountdown>0)
        attackCountdown -= Time.fixedDeltaTime;

        if (goal != null && stunTimeRemaining <= 0)
        {
            animator.SetBool("Walk", true);
            Vector2 direction = (goal.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            
            stunTimeRemaining -= Time.fixedDeltaTime;
        }

        if (target != null && stunTimeRemaining <= 0)
        {
            animator.SetBool("Walk", true);
            Vector2 direction = (target.transform.position - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            
            stunTimeRemaining -= Time.fixedDeltaTime;
        }
    }

    public void Stun()
    {
        stunTimeRemaining = stunDuration;
        animator.SetBool("Walk", false);
    }

    public void Stun(float stunDuration)
    {
        stunTimeRemaining = stunDuration;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Vector2 bounceDirection = (transform.position - collision.transform.position).normalized;
            rb.AddForce(bounceDirection * bounceForce, ForceMode2D.Impulse);
            collision.collider.GetComponent<Enemy>().Stun(impactStun);
            collision.collider.GetComponent<Rigidbody2D>().AddForce(-bounceDirection * bounceForce *impactBounce , ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") || collision.CompareTag("Home"))
        {
            
            if (attackCountdown <= 0)
            {
                
                collision.GetComponent<HealthSystem>().TakeDamage(damage);
                attackCountdown = attackCooldown;
            }
           

        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isChasing)
        {
            if (attackCountdown <= 0)
            {
                collision.GetComponent<HealthSystem>().TakeDamage(damage);
                attackCountdown = attackCooldown;
            }
        }
        else if(collision.CompareTag("Home") && !isChasing)
        {
            if (attackCountdown <= 0)
            {
                collision.GetComponent<HealthSystem>().TakeDamage(damage);
                attackCountdown = attackCooldown;
            }
        }
    }
}