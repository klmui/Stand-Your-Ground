using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private GameObject fireExplosion;
    [SerializeField] private Collider ballHitbox;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private FireballRespawnSlot respawnSlot;

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

        fireExplosion.SetActive(true);
        fireExplosion.transform.position = transform.position;

        fireExplosion.transform.parent = null;

        respawnSlot.FireballExploded();

        Destroy(gameObject);
    }

    public void OnGrabbed()
    {
        Debug.Log("Grabbed!");

        rb.useGravity = true;

        respawnSlot.FireballGrabbed();
    }

    public void OnThrown()
    {
        gameObject.layer = 10;
    }

    public void SetSlot(FireballRespawnSlot newSlot)
    {
        respawnSlot = newSlot;
    }
}
