using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitboxToWeapon : MonoBehaviour
{
    private AggressiveWeapon weap;

    private void Awake()
    {
        weap = GetComponentInParent<AggressiveWeapon>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        weap.AddToDetected(other);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        weap.RemoveFromDetected(collision);
    }
}
