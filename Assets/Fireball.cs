using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private GameObject fireExplosion;
    [SerializeField] private Collider ballHitbox;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit trigger: " + other.gameObject.name);
        KABOOM();
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit collider: " + collision.gameObject.name);
        KABOOM();
    }

    public void KABOOM()
    {
        ballHitbox.enabled = false;

        GameObject boomObj = Instantiate(fireExplosion);
        boomObj.transform.position = transform.position;

        boomObj.transform.parent = null;
        Destroy(gameObject);
    }
}
