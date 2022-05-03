using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    [SerializeField] private Collider hitbox;

    public void EnableHitbox()
    {
        hitbox.enabled = true;
    }
}
