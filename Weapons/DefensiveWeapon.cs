using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveWeapon : Weapon
{
    protected SO_DefensiveWeapon defensiveWeaponData;

    public float startTime { get; private set; }
    public float parryTime { get; private set; }

    protected override void Awake()
    {
        weapAnimator = transform.Find("Shield").GetComponent<Animator>();
        playerAttackAnimator = transform.Find("DefensePlayer").GetComponent<Animator>();

        gameObject.SetActive(false);

        if (IsShield())
        {
            defensiveWeaponData = (SO_DefensiveWeapon)weapData;
            parryTime = 0.2f;
        }
        else
        {
            Debug.Log("ERROR: Wrong data for the weapon!");
        }       
    }

    public override void EnterWeapon()
    {
        gameObject.SetActive(true);

        playerAttackAnimator.SetBool("defend", true);
        weapAnimator.SetBool("defend", true);

        startTime = Time.time;
    }

    public override void ExitWeapon()
    {
        playerAttackAnimator.SetBool("defend", false);
        weapAnimator.SetBool("defend", false);

        gameObject.SetActive(false);
    }

    public override void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weapData.movementSpeeds[0]);
    }

    public bool CanParry()
    {
        return Time.time >= startTime + parryTime;
    }
}
