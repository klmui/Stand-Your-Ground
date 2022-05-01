using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField] private Transform objToFollow;
    [SerializeField] private bool ignoreY;

    // Update is called once per frame
    void Update()
    {
        Vector3 targPos = objToFollow.position;

        if (ignoreY)
            targPos.y = transform.position.y;

        transform.position = targPos;
    }
}
