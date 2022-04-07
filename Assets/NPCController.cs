using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float frictionStrength;
    [SerializeField] private float accelSpeed;

    [Header("References")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Animator anim;
    //[SerializeField] private Rigidbody rb;
    [SerializeField] private Transform PathParent;
    private List<Vector3> path = new List<Vector3>();

    private Vector3 target;
    private bool pathFinished = false;

    private Vector3 lastPos;

    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        if(PathParent != null)
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
        if (pathFinished)
            return;

        Vector3 currMove = transform.position - lastPos;
        //Debug.Log(currMove.magnitude);
        anim.SetFloat("Speed", currMove.magnitude / (agent.speed*Time.deltaTime));
        lastPos = transform.position;

        Vector2 dist = new Vector2(transform.position.x - target.x, transform.position.z - target.z);
        if (dist.magnitude < 0.1f)
            GetNewTarget();
    }
}
