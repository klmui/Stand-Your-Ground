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
            Debug.Log("TAKE DMG");
            Hero_Stats.Instance.TakeDamage(1);
        }
    }
}
