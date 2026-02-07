using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [Header("Components")]
    public NavMeshAgent agent;
    public Animator anim;
    public Transform player; // Only used for distance check and damage
    
    [Header("Audio")]
    public AudioSource zombieAudio;
    public AudioClip screamSound;
    
    [Header("Behavior Settings")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackRange = 2f;
    public float attackCooldown = 3f;
    
    [Header("Patrol Settings")]
    public float patrolRadius = 15f; // How far from spawn point to wander
    public float patrolWaitTime = 3f; // Wait at each patrol point
    
    private Vector3 spawnPoint;
    private Vector3 currentTarget;
    private bool isAttacking = false;
    private float lastAttackTime = 0f;
    private bool isChasing = false; // NEW: Track if zombie is actively chasing
    private float patrolTimer = 0f;

    void Start()
    {
        spawnPoint = transform.position;
        agent.speed = patrolSpeed;
        SetRandomPatrolPoint();
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // ONLY CHASE if we heard a sound (isChasing = true)
        if (isChasing)
        {
            agent.SetDestination(player.position);
            
            // Update animation
            if (agent.velocity.magnitude > 0.1f)
            {
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isRunning", false);
            }
        }
        else
        {
            // PATROL MODE - wander around spawn point
            Patrol();
        }

        // ATTACK if close enough (regardless of chase/patrol mode)
        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                AttackPlayer();
            }
        }
    }

    void Patrol()
    {
        // Check if reached patrol point
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            patrolTimer += Time.deltaTime;
            
            // Wait at patrol point, then pick new one
            if (patrolTimer >= patrolWaitTime)
            {
                SetRandomPatrolPoint();
                patrolTimer = 0f;
            }
            
            // Idle animation when waiting
            anim.SetBool("isRunning", false);
        }
        else
        {
            // Walking animation when moving
            if (agent.velocity.magnitude > 0.1f)
            {
                anim.SetBool("isRunning", true);
            }
        }
    }

    void SetRandomPatrolPoint()
    {
        // Pick random point around spawn location
        Vector2 randomCircle = Random.insideUnitCircle * patrolRadius;
        Vector3 randomPoint = spawnPoint + new Vector3(randomCircle.x, 0, randomCircle.y);
        
        // Make sure it's on NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas))
        {
            currentTarget = hit.position;
            agent.SetDestination(currentTarget);
            agent.speed = patrolSpeed;
        }
    }

    void AttackPlayer()
    {
        isAttacking = true;
        lastAttackTime = Time.time;

        // Stop moving
        agent.isStopped = true;

        // Play scream
        if (zombieAudio && screamSound)
        {
            zombieAudio.PlayOneShot(screamSound);
        }

        // Play attack animation
        anim.SetTrigger("attack");

        // Deal damage
        PlayerHealth pHealth = player.GetComponent<PlayerHealth>();
        if (pHealth != null)
        {
            pHealth.TakeDamage(25);
            Debug.Log("Zombie attacked! Player took 25 damage.");
        }

        // Resume behavior after attack
        Invoke("ResumeChase", 2f);
    }

    void ResumeChase()
    {
        isAttacking = false;
        agent.isStopped = false;
    }

    // Called by SoundManager when player makes noise
    public void HearSound(Vector3 noisePosition)
    {
        if (isAttacking) return; // Don't interrupt attack

        Debug.Log("🧟 Zombie heard noise at: " + noisePosition);
        
        // NOW the zombie knows where player is
        isChasing = true;
        
        // Move toward the noise
        agent.SetDestination(noisePosition);
        agent.speed = chaseSpeed;
        anim.SetBool("isRunning", true);
        
        // Chase for a limited time, then go back to patrol
        CancelInvoke("StopChasing"); // Cancel any previous timer
        Invoke("StopChasing", 10f); // Chase for 10 seconds after last sound
    }

    void StopChasing()
    {
        Debug.Log("🧟 Zombie lost track of player. Returning to patrol.");
        isChasing = false;
        agent.speed = patrolSpeed;
        SetRandomPatrolPoint();
    }

    // Optional: Draw patrol radius in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Application.isPlaying ? spawnPoint : transform.position, patrolRadius);
    }
}