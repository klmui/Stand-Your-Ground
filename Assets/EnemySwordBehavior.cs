using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySwordBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "MainCamera")
        {
            Hero_Stats.Instance.TakeDamage(1);
            VibrationManager.singleton.TriggerVibration(40, 2, 255, OVRInput.Controller.LTouch);
            VibrationManager.singleton.TriggerVibration(40, 2, 255, OVRInput.Controller.RTouch);
        }
    }
}
