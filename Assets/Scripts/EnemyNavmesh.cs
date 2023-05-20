using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavmesh : MonoBehaviour
{
    public Animator anim;
    [SerializeField] Transform[] waypoints;
    int index;
    Vector3 target;
    private NavMeshAgent navmeshAgent;
    private bool followPlayer = false;
    [SerializeField] Transform playerPos;
    [SerializeField] Transform player;
    public bool isDead = false;
    public BoxCollider leftHand;
    public BoxCollider rightHand;
    public static bool attack;
    public bool rotate;
    public GameObject Player;
    public bool talk;
    public bool canvasActive;
    public GameObject canvas;

    private void Awake()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        UpdateDestination();
    }

    void Update()
    {
        if (!isDead)
        {
            if (!followPlayer)
            {
                if (Vector3.Distance(transform.position, target) < 1)
                {
                    IterateWaypoint();
                    UpdateDestination();
                }
            }
            else
            {
                FollowPlayer();
            }
            if (attack)
                StartCoroutine(startAttack());
        }
    }

    IEnumerator startAttack()
    {
        attack = false;
        this.anim.SetBool("Punch", true);
        rightHand.enabled = true;
        leftHand.enabled = true;
        yield return new WaitForSeconds(1);
        this.anim.SetBool("Punch", false);
        rightHand.enabled = false;
        leftHand.enabled = false;
        yield return new WaitForSeconds(2);
        if(Player.gameObject.GetComponent<Player>().Health > 0)
        {
            attack = true;
        }
        else
        {
            attack = false;
            StopCoroutine(startAttack());
        }
        if(talk)
        {
            anim.SetBool("Talk", true);
        }
        else
        {
            anim.SetBool("Talk", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerArea")
        {
            followPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "PlayerArea")
        {
            followPlayer = false;
        }
    }

    void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, playerPos.position) > 0.1f)
        {
            anim.SetBool("isWalking", true);
            navmeshAgent.SetDestination(playerPos.position);
        }
        else
        {
            anim.SetBool("isWalking", false);
            RotateTowards(player);
            if(!canvasActive)
            {
                canvas.SetActive(true);
                canvasActive = true;
            }
        }
    }

    private void RotateTowards(Transform target)
    {
        if (rotate)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 2f);
            rotate = false;
        }
    }

    void UpdateDestination()
    {
        anim.SetBool("isWalking", true);
        target = waypoints[index].position;
        navmeshAgent.SetDestination(target);
    }

    void IterateWaypoint()
    {
        index++;
        if (index == waypoints.Length)
            index = 0;
    }
}
