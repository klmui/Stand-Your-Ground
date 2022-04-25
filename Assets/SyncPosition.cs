using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPosition : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}
