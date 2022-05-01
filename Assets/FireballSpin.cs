using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpin : MonoBehaviour
{
    [SerializeField] private Vector3 spinSpeeds;

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles += spinSpeeds * Time.deltaTime;
        transform.localPosition = Vector3.zero;
    }
}
