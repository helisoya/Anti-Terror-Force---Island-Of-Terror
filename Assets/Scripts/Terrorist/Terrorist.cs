using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Terrorist : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private string gunDropName;


    [Header("Animation")]
    [SerializeField] private Animator animator;

    [Header("Movement")]
    [SerializeField] private NavMeshAgent agent;
    private Vector3 lastKnownPlayerPos;
    [SerializeField] private SpriteRenderer indicator;
    private bool walking;
    private float currentWaitTime;
    [SerializeField] private float maxWaitTime;
    [SerializeField] private Transform[] waypoints;

    [SerializeField] private float speedNormal;
    [SerializeField] private float speedRunning;


    [Header("Sight")]
    [SerializeField] private FieldOfView fov;
    public bool hasSeenPlayer = false;
    [SerializeField] private float maxSeenTime;
    private float currentSeenTime;

    private PlayerHealth player;


    [Header("Shooting")]
    [SerializeField] private Transform gunBarrel;
    [SerializeField] private ParticleSystem gunFlash;
    [SerializeField] private int damage;
    [SerializeField] private float maxFireTime;
    private float currentFireTime;
    [SerializeField] private float fireRange;

    [SerializeField] private AudioSource audioSource;



    private float distanceToPlayer
    {
        get
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }
    }


    public float fillAmountIndicator
    {
        get
        {
            if (hasSeenPlayer) return 0;
            return (maxSeenTime - currentSeenTime) / maxSeenTime;
        }
    }

    void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
    }


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fov.radius);
    }


    void PlayerIsSeen()
    {
        hasSeenPlayer = true;
        animator.SetBool("alert", true);
        lastKnownPlayerPos = player.transform.position;
        agent.speed = speedRunning;
    }

    void LookAt(Transform obj)
    {
        transform.LookAt(new Vector3(obj.position.x, transform.position.y, obj.position.z));
    }

    void Update()
    {

        if (currentFireTime > 0)
        {
            currentFireTime -= Time.deltaTime;
        }


        if (hasSeenPlayer)
        {
            if (fov.canSeePlayer)
            {
                currentSeenTime = maxSeenTime;
                lastKnownPlayerPos = player.transform.position;
                agent.SetDestination(lastKnownPlayerPos);

                if (distanceToPlayer <= fireRange && currentFireTime <= 0)
                {
                    currentFireTime = maxFireTime;
                    gunFlash.Play();
                    audioSource.Play();
                    if (Random.Range(0, 7) <= 2)
                    {
                        player.AddHealth(-damage);
                    }
                }
            }
            else
            {
                currentSeenTime -= Time.deltaTime;
                if (currentSeenTime <= 0)
                {
                    hasSeenPlayer = false;
                    animator.SetBool("alert", false);
                    agent.speed = speedNormal;
                }
            }
        }
        else
        {
            if (distanceToPlayer <= 2f)
            {
                PlayerIsSeen();
                LookAt(player.transform);
            }


            if (fov.canSeePlayer)
            {
                if (walking)
                {
                    walking = false;
                    currentWaitTime = maxWaitTime;
                    agent.SetDestination(transform.position);
                }

                LookAt(player.transform);
                currentSeenTime -= Time.deltaTime;
                if (currentSeenTime <= 0)
                {
                    PlayerIsSeen();
                }
            }
            else
            {
                currentSeenTime = maxSeenTime;

                if (walking && agent.remainingDistance <= 0.1f)
                {
                    walking = false;
                    currentWaitTime = maxWaitTime;
                }
                else if (!walking)
                {
                    currentWaitTime -= Time.deltaTime;
                    if (currentWaitTime <= 0)
                    {
                        walking = true;
                        agent.SetDestination(waypoints[Random.Range(0, waypoints.Length)].position);
                    }
                }
            }
        }

        animator.SetBool("walk", agent.remainingDistance > 0.1f);
    }



    public void TakeDamage(int value)
    {
        health -= value;
        if (health <= 0)
        {
            StartCoroutine(WaitDeath());
        }
        else
        {
            if (!hasSeenPlayer)
            {
                PlayerIsSeen();
            }
            LookAt(player.transform);
        }
    }


    IEnumerator WaitDeath()
    {
        animator.SetTrigger("dead");
        GetComponent<Collider>().enabled = false;
        Instantiate(Resources.Load<GameObject>("Guns_Drop/" + gunDropName), transform.position, Quaternion.identity);
        agent.isStopped = true;
        enabled = false;
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
}
