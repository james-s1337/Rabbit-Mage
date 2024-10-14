using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;

    private List<IDamagable> detectedDamagable = new List<IDamagable>();

    protected override void Awake()
    {
        base.Awake();

        if (weapData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData)weapData;
        }
        else
        {
            Debug.Log("ERROR: Wrong data for the weapon!");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackNum];
        if (isSkill)
        {
            details = aggressiveWeaponData.ComboDetails[skillNum];
        }

        foreach (IDamagable item in detectedDamagable.ToList())
        {
            item.Damage(details.pDamageAmount, details.mDamageAmount);
        }
    }
    public void AddToDetected(Collider2D collision)
    {
        IDamagable damageable = collision.GetComponent<IDamagable>();
 
        if (damageable != null)
        {
            detectedDamagable.Add(damageable);
        }
    }

    public void RemoveFromDetected(Collider2D collision)
    {
        IDamagable damageable = collision.GetComponent<IDamagable>();

        if (damageable != null)
        {
            detectedDamagable.Remove(damageable);
        }
    }
}
