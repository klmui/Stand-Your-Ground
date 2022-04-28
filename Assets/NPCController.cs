using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [SerializeField] private LayerMask terrainLayer;

    [System.Serializable] public enum NPCStateEnum
    {
        pathing,
        aggrod,
        attacking,
        dead
    }

    [SerializeField] private NPCStateEnum state;
    public NPCStateEnum State => state;

    [Header("Movement Properties")]
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float frictionStrength;
    [SerializeField] private float accelSpeed;

    [Header("Strafe Properties")]
    [SerializeField] private float minStrafeSpeedPercent;
    [SerializeField] private float maxStrafeSpeed;
    [SerializeField] private float minRandStrafeTime;
    [SerializeField] private float maxRandStrafeTime;
    [SerializeField] private float targetDistanceToPlayer;
    private float nextStrafeSwapTime;
    private float strafeSpeed;

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
    [SerializeField] private Transform pathParent;

    [SerializeField] private Collider swordHitbox;

    private List<Vector3> path;

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

        /*if(path == null)
            path = new List<Vector3>();

        if (pathParent != null)
        {
            for(int i = 0; i< pathParent.childCount; i++)
            {
                Transform currTarget = pathParent.GetChild(i);

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
            //transform.position = new Vector3(startPos.x, transform.position.y, startPos.z);
            //path.Add(new Vector3(targetPos.x, transform.position.y, targetPos.z));
        }

        GetNewTarget();*/
    }

    public void SetPath(Transform newPathParent)
    {
        Debug.Log("Set path");

        path = new List<Vector3>();

        pathParent = newPathParent.transform;
        Debug.Log(pathParent.name);

        if (pathParent != null)
        {
            for (int i = 0; i < pathParent.childCount; i++)
            {
                Transform currTarget = pathParent.GetChild(i);

                RaycastHit hit;
                if (Physics.Raycast(currTarget.position, Vector3.down, out hit, 10f, terrainLayer))
                {
                    path.Add(hit.point);
                    //currTarget.position = hit.point;
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
            case NPCStateEnum.dead:
                {
                    return;
                }

            case NPCStateEnum.pathing:
                {
                    //Check if close enough to player to aggro
                    Vector2 distToPlayer = new Vector2(transform.position.x - playerTransform.position.x, transform.position.z - playerTransform.position.z);
                    if(distToPlayer.magnitude <= distanceToAggro)
                    {
                        state = NPCStateEnum.aggrod;
                        SetNextPossibleAttackTime();

                        pathFinished = true;

                        nextStrafeSwapTime = Time.time + 0.25f;

                        anim.SetTrigger("StartStrafe");

                        agent.SetDestination(transform.position);

                        /*//Stop navmesh agent
                        agent.SetDestination(transform.position);
                        agent.isStopped = true;
                        anim.SetFloat("Speed", 0);

                        //Look at player
                        transform.LookAt(playerTransform);

                        transform.eulerAngles = new Vector3(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);*/

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
                    //Set strafe speed
                    if(Time.time >= nextStrafeSwapTime)
                    {
                        float nextStrafeSpeed = Mathf.Max(minStrafeSpeedPercent, Random.Range(0, 1));
                        float sign = Random.Range(-1, 1);
                        if (sign >= 0)
                            sign = 1;
                        else
                            sign = -1;

                        strafeSpeed = nextStrafeSpeed * sign * maxStrafeSpeed;

                        nextStrafeSwapTime = Time.time + Random.Range(minRandStrafeTime, maxRandStrafeTime);
                    }

                    if(Time.time >= nextAttackTime) //Time to start new attack (or at least check)
                    {
                        if(NPCManager.Instance.CanNewEnemyAttack())
                        {
                            //Start Attack
                            attackController.DoRandomAttack();
                            state = NPCStateEnum.attacking;
                            swordHitbox.enabled = true;

                            //Stop strafing
                            agent.SetDestination(transform.position);
                            agent.speed = 0;

                            //Look at player
                            Vector3 lookPos = playerTransform.position - transform.position;
                            lookPos.y = 0;
                            Quaternion rot = Quaternion.LookRotation(lookPos);
                            transform.rotation = rot;

                            return;
                        }
                        else
                        {
                            //Can't attack now, get new attack time
                            SetNextPossibleAttackTime();
                        }
                    }
                    else //Circle around player
                    {
                        //get direction to player
                        Vector3 dir = (playerTransform.position - transform.position).normalized;
                        dir.y = 0;

                        //spir dir left or right depending on strafe speed
                        dir = Quaternion.Euler(0, 90, 0) * dir;
                        dir *= strafeSpeed;

                        //Adjust target position based on distance to player
                        float distanceAway = (playerTransform.position - transform.position).magnitude;
                        float distDiff = distanceAway - targetDistanceToPlayer;
                        Vector3 toPlayer = (playerTransform.position - transform.position).normalized * distDiff;
                        toPlayer.y = 0;

                        dir += toPlayer;

                        //Move in target direction
                        agent.SetDestination(transform.position + dir);

                        //Look at player
                        Vector3 lookPos = playerTransform.position - transform.position;
                        lookPos.y = 0;
                        Quaternion rot = Quaternion.LookRotation(lookPos);
                        transform.rotation = rot;

                        //Set animation values + navmesh Speed
                        float strafeSpeedLerped = Mathf.Lerp(anim.GetFloat("StrafeSpeed"), strafeSpeed, Time.deltaTime*8);
                        agent.speed = Mathf.Abs(strafeSpeedLerped);
                        anim.SetFloat("StrafeSpeed", strafeSpeedLerped);
                    }

                    break;
                }
            case NPCStateEnum.attacking:
                {
                    //Make sure not moving
                    agent.speed = 0;

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
        //Debug.Log("Check Attack Done");

        if(state == NPCStateEnum.attacking)
        {
            state = NPCStateEnum.aggrod;
            NPCManager.Instance.EnemyDoneAttack();

            swordHitbox.enabled = false;

            SetNextPossibleAttackTime();
        }
    }

    public void GotHit(bool killed)
    {
        //Reset attack triggers
        attackController.GotHit();

        //If attacking, set state to aggro'd
        CheckAttackDone();

        if (killed)
            state = NPCStateEnum.dead;
    }

    public void LookAtPlayerAttacking()
    {
        /*if (state != NPCStateEnum.attacking)
            Debug.LogError("bruh");

        state = NPCStateEnum.attacking;

        //Look at player
        Vector3 lookPos = playerTransform.position - transform.position;
        lookPos.y = 0;
        Quaternion rot = Quaternion.LookRotation(lookPos);
        transform.rotation = rot;*/
    }
}
