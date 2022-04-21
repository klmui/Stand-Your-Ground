using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [System.Serializable] public enum NPCStateEnum
    {
        pathing,
        aggrod,
        attacking
    }

    [SerializeField] private NPCStateEnum state;
    public NPCStateEnum State => state;

    [Header("Movement Properties")]
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float frictionStrength;
    [SerializeField] private float accelSpeed;

    [Header("Attack Properties")]
    [SerializeField] private float distanceToAggro;
    [SerializeField] private float minRandAttackTime;
    [SerializeField] private float maxRandAttackTime;
    private float nextAttackTime;

    [Header("References")]
    [SerializeField] private NPCAttackController attackController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private Transform PathParent;

    [SerializeField] private Collider swordHitbox;

    private List<Vector3> path = new List<Vector3>();

    private Vector3 target;
    private bool pathFinished = false;

    private Vector3 lastPos;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 targetPos;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        if (playerTransform == null)
        {
            playerTransform = NPCManager.Instance.PlayerTransform;
        }

        if (PathParent != null)
        {
            for(int i = 0; i< PathParent.childCount; i++)
            {
                Transform currTarget = PathParent.GetChild(i);

                RaycastHit hit;
                if(Physics.Raycast(currTarget.position, Vector3.down, out hit))
                {
                    path.Add(hit.point);
                    currTarget.position = hit.point;
                }
            }
        }
        else
        {
            transform.position = new Vector3(startPos.x, transform.position.y, startPos.z);

            path.Add(new Vector3(targetPos.x, transform.position.y, targetPos.z));
        }

        GetNewTarget();
    }

    public void GetNewTarget()
    {
        if (path.Count > 0)
        {
            target = path[0];
            path.RemoveAt(0);
            agent.SetDestination(target);
        }
        else
        {
            pathFinished = true;
            agent.isStopped = true;
            anim.SetFloat("Speed", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case NPCStateEnum.pathing:
                {
                    //Check if close enough to player to aggro
                    Vector2 distToPlayer = new Vector2(transform.position.x - playerTransform.position.x, transform.position.z - playerTransform.position.z);
                    if(distToPlayer.magnitude <= distanceToAggro)
                    {
                        state = NPCStateEnum.aggrod;
                        SetNextPossibleAttackTime();

                        //Stop navmesh agent
                        pathFinished = true;
                        agent.isStopped = true;
                        anim.SetFloat("Speed", 0);

                        return;
                    }

                    //Idle at end of path
                    if (pathFinished)
                        return;

                    //Moving along path, set animation vars
                    Vector3 currMove = transform.position - lastPos;
                    anim.SetFloat("Speed", currMove.magnitude / (agent.speed * Time.deltaTime));
                    lastPos = transform.position;

                    //Check if reached target
                    Vector2 dist = new Vector2(transform.position.x - target.x, transform.position.z - target.z);
                    if (dist.magnitude < 0.1f)
                        GetNewTarget();

                    break;
                }
            case NPCStateEnum.aggrod:
                {
                    if(Time.time >= nextAttackTime)
                    {
                        if(NPCManager.Instance.CanNewEnemyAttack())
                        {
                            //Start Attack
                            attackController.DoRandomAttack();
                            state = NPCStateEnum.attacking;
                            swordHitbox.enabled = true;
                        }
                        else
                        {
                            //Can't attack now, get new attack time
                            SetNextPossibleAttackTime();
                        }
                    }
                    else
                    {
                        //Circle around player or something
                    }

                    break;
                }
            case NPCStateEnum.attacking:
                {
                    break;
                }
        }
    }

    public void SetNextPossibleAttackTime()
    {
        nextAttackTime = Time.time + Random.Range(minRandAttackTime, maxRandAttackTime);
    }

    public void CheckAttackDone()
    {
        Debug.Log("Check Attack Done");

        if(state == NPCStateEnum.attacking)
        {
            state = NPCStateEnum.aggrod;
            NPCManager.Instance.EnemyDoneAttack();

            swordHitbox.enabled = false;

            SetNextPossibleAttackTime();
        }
    }

    public void GotHit()
    {
        //Reset attack triggers
        attackController.GotHit();

        //If attacking, set state to aggro'd
        CheckAttackDone();
    }
}
