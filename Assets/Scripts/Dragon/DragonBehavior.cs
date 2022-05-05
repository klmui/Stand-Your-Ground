using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonBehavior : MonoBehaviour
{
    public enum DragonState
    {
        idle,
        walking,
        attacking,
        dead
    }
    private DragonState state;

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
    private float nextStrafeSwapTime;
    private float strafeSpeed;

    [Header("Attack Properties")]
    [SerializeField] private float minRandAttackTime;
    [SerializeField] private float maxRandAttackTime;
    private float nextAttackTime;

    [Header("References")]
    [SerializeField] private Animator anim;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Collider biteHitbox;
    [SerializeField] private Collider clawHitbox;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private ParticleSystem flamethrower;

    [SerializeField] private GameObject fireHitbox;

    private Vector3 lastPos;

    private int lastAttackIndex;

    [System.Serializable] public struct attack
    {
        public string triggerName;
        public float targDistance;
    }
    [SerializeField] private attack[] attacks;

    private attack nextAttack;

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        state = DragonState.idle;

        GetNextAttack();
    }

    public void DoneIntro()
    {
        state = DragonState.walking;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case DragonState.idle:
                {
                    break;
                }

            case DragonState.walking:
                {
                    //Set anim speed
                    Vector3 currMove = transform.position - lastPos;
                    anim.SetFloat("Speed", 1);
                    lastPos = transform.position;


                    //Set strafe speed
                    if (Time.time >= nextStrafeSwapTime)
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

                    if (Time.time >= nextAttackTime) //Time to start new attack (or at least check)
                    {
                        if (InRange())
                        {
                            //Start Attack
                            DoAttack();
                            state = DragonState.attacking;

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
                            //nextAttackTime = Time.time + Random.Range(minRandAttackTime, maxRandAttackTime);
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
                        float distDiff = distanceAway - nextAttack.targDistance;
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
                        //float strafeSpeedLerped = Mathf.Lerp(anim.GetFloat("StrafeSpeed"), strafeSpeed, Time.deltaTime * 8);
                        agent.speed = Mathf.Abs(strafeSpeed);
                        //anim.SetFloat("Speed", Mathf.Abs(strafeSpeed));
                    }

                    break;
                }
            case DragonState.attacking:
                {
                    break;
                }
            case DragonState.dead:
                {
                    break;
                }
        }
    }

    public void DoAttack()
    {
        anim.SetTrigger(nextAttack.triggerName);

        GetNextAttack();
    }

    private void GetNextAttack()
    {
        //Make next attack unique from the last one
        int nextAttackIndex = Random.Range(0, attacks.Length);
        while (nextAttackIndex == lastAttackIndex)
        {
            nextAttackIndex = Random.Range(0, attacks.Length);
        }

        nextAttack = attacks[nextAttackIndex];
        lastAttackIndex = nextAttackIndex;

        nextAttackTime = Time.time + Random.Range(minRandAttackTime, maxRandAttackTime);
    }

    public bool InRange()
    {
        Vector3 dragPos = transform.position;
        Vector3 playerPos = playerTransform.position;
        dragPos.y = 0;
        playerPos.y = 0;

        if (Mathf.Abs((dragPos - playerPos).magnitude - nextAttack.targDistance) <= 5)
            return true;

        return false;
    }

    public void AttackDone()
    {
        if (state == DragonState.attacking)
            state = DragonState.walking;
    }

    public void Die()
    {
        state = DragonState.dead;
        agent.speed = 0;
        anim.SetTrigger("Die");
    }

    public void EnableClawHitbox()
    {
        clawHitbox.enabled = true;
    }

    public void DisableClawHitbox()
    {
        clawHitbox.enabled = false;
    }

    public void EnableBiteHitbox()
    {
        biteHitbox.enabled = true;
    }

    public void DisableBiteHitbox()
    {
        biteHitbox.enabled = false;
    }

    public void EnableFire()
    {
        flamethrower.Play();
        fireHitbox.SetActive(true);
    }

    public void DisableFire()
    {
        flamethrower.Stop();
        fireHitbox.SetActive(false);
    }

    public void DragonKilled()
    {
        GameManager.Instance.Victory();
    }
}


