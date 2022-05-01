using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpin : MonoBehaviour
{
    [SerializeField] private bool dontMatchPos;
    [SerializeField] private Vector3 spinSpeeds = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        //Vector3 spinSpdFrame = spinSpeeds * Time.deltaTime;
        transform.Rotate(spinSpeeds * Time.deltaTime, Space.Self);
        //transform.localEulerAngles = new Vector3(transform.localEulerAngles.x + spinSpeeds.x * Time.deltaTime, transform.localEulerAngles.y + spinSpeeds.y * Time.deltaTime, transform.localEulerAngles.z + spinSpeeds.z * Time.deltaTime);
        if(dontMatchPos == false)
            transform.localPosition = Vector3.zero;
    }
}
