using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Health = 100f;
    private Animator animator;
    public GameObject Player;
    public PlayerController PlayerController;
    public bool isDead = false;
    private float distance;
    private float attackCooldown = 0f;

    public event System.Action OnAttack;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !isDead)
        {
            distance = Vector3.Distance(transform.position, Player.transform.position);
            animator.SetBool("PlayerInSight", true);
            animator.SetBool("ApproachPlayer", true);
            transform.LookAt(Player.transform);
            if (distance >= 2.0f)
            {
                transform.position += transform.forward * Time.deltaTime * 0.5f;
            }
            else if (distance < 2.0f)
            {
                animator.SetBool("ApproachPlayer", false);
                attackCooldown -= Time.deltaTime;
                Attack(PlayerController);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && !isDead)
        {
            animator.SetBool("ApproachPlayer",false);
            animator.SetBool("PlayerInSight", false);
        }
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;
        if (Health <= 0f)
        {
            Health = 0;
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        animator.SetBool("IsDead", true);
    }

    public void Attack(PlayerController player)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(player, 2f));

            if (OnAttack != null)
            {
                OnAttack();
            }
            attackCooldown = 2f / 1f;
        }
    }

    private IEnumerator DoDamage(PlayerController player, float delay)
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(delay);
        if (distance < 2.0f)
        {
            player.TakeDamage(20f);
        }
    }
}
