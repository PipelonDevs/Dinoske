using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    ScriptableObject initialValues;
    public float attackForce = 10.0f;
    public float attackWideForce = 10.0f;
    public float attackCooldown = 1.0f;
    public float attackWideCooldown = 1.0f;
    public CapsuleCollider2D attackHitbox;
    public CapsuleCollider2D attackWideHitbox;
    public LayerMask enemyLayer;
    public int damage = 10;
    public int damageWide = 10;

    private float timeSinceLastAttack;
    private float timeSinceLastWideAttack;
    private Animator animator;
    private Animator attackAnimator;
    private Animator attackWideAnimator;
    TopDownCharacterController controller;
    ExperienceSystem experienceSystem;
   


    void Awake()
    {
        experienceSystem = GetComponent<ExperienceSystem>();
        controller = GetComponent<TopDownCharacterController>();
        animator = GetComponent<Animator>();
        attackAnimator = attackHitbox.GetComponent<Animator>();
        attackWideAnimator = attackWideHitbox.GetComponent<Animator>();
        
        timeSinceLastAttack = attackCooldown;
        timeSinceLastWideAttack = attackWideCooldown;
    }
    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        timeSinceLastWideAttack += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && timeSinceLastAttack >= attackCooldown)
        {
            StartCoroutine(AttackCooldown());
            animator.SetTrigger("Attack");
            attackAnimator.SetTrigger("Attack");
            Attack();
            timeSinceLastAttack = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q) && timeSinceLastWideAttack >= attackWideCooldown && timeSinceLastAttack>=0.61f)
        {
            StartCoroutine(AttackCooldown());
            animator.SetTrigger("AttackWide");
            attackWideAnimator.SetTrigger("AttackWide");
            WideAttack();
            timeSinceLastWideAttack = 0;
        }
       
    }

    void Attack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCapsuleAll(attackHitbox.bounds.center, attackHitbox.size* attackHitbox.transform.localScale, attackHitbox.direction, 0, enemyLayer);
      
        foreach (Collider2D enemyCollider in enemiesHit)
        {
            Rigidbody2D enemyRb = enemyCollider.GetComponent<Rigidbody2D>();
            Enemy enemy = enemyCollider.GetComponent<Enemy>();

            if (enemyRb != null && enemy != null)
            {

                bool isKilled = enemy.GetComponent<HealthSystem>().TakeDamage(damage);
                if(isKilled)
                {
                    experienceSystem.addExperience(enemy.experience);
                }
                Vector2 pushDirection = (enemyCollider.transform.position - transform.position).normalized;
                enemy.Stun();
                enemyRb.AddForce(pushDirection * attackForce, ForceMode2D.Impulse);
              
            }
        }
    }

    void WideAttack()
    {
        Collider2D[] enemiesHit = Physics2D.OverlapCapsuleAll(attackWideHitbox.bounds.center, attackWideHitbox.size* attackWideHitbox.transform.localScale, attackWideHitbox.direction, 0, enemyLayer);
        Debug.Log("Wide Attack");
        foreach (Collider2D enemyCollider in enemiesHit)
        {
            Rigidbody2D enemyRb = enemyCollider.GetComponent<Rigidbody2D>();
            Enemy enemy = enemyCollider.GetComponent<Enemy>();

            if (enemyRb != null && enemy != null)
            {

                bool isKilled= enemy.GetComponent<HealthSystem>().TakeDamage(damageWide);
                if (isKilled)
                {
                    experienceSystem.addExperience(enemy.experience);
                }
                Vector2 pushDirection = (enemyCollider.transform.position - transform.position).normalized;
                enemy.Stun();
                enemyRb.AddForce(pushDirection * attackWideForce, ForceMode2D.Impulse);

            }
        }
    }

    IEnumerator AttackCooldown()
    {
        controller.canMove = false;
        yield return new WaitForSeconds(0.35f);
        controller.canMove = true;
    }

    public void addAttackDmg(int dmg)
    {
        damage += dmg;
    }

    public void addAttackWideDmg(int dmg)
    {
        damageWide += dmg;
    }

    public void addAttackForce(float force)
    {
        attackForce += force;
    }

    public void addAttackWideForce(float force)
    {
        attackWideForce += force;
    }

    public void addAttackCooldown(float cooldown)
    {
        attackCooldown -= cooldown;
    }

    public void addAttackWideCooldown(float cooldown)
    {
        attackWideCooldown -= cooldown;
    }

    public void addAttackHitboxSize(float size)
    {
        attackHitbox.transform.localScale += new Vector3(size, size, size);
    }

    public void addAttackWideHitboxSize(float size)
    {
        attackWideHitbox.transform.localScale += new Vector3(size, size, size);
    }

    public void addMovementSpeed(float speed)
    {
        controller.speed += speed;
    }


}