using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationToWeapon : MonoBehaviour
{
    private Weapon weap;

    private void Start()
    {
        weap = GetComponentInParent<Weapon>();
    }

    private void AnimationFinishTrigger()
    {
        weap.AnimationFinishTrigger();
    }

    private void AnimationStartMovementTrigger()
    {
        weap.AnimationStartMovementTrigger();
    }

    private void AnimationStopMovementTrigger()
    {
        weap.AnimationStopMovementTrigger();
    }

    private void AnimationTurnOffFlipTrigger()
    {
        weap.AnimationTurnOffFlipTrigger();
    }

    private void AnimationTurnOnFlipTrigger()
    {
        weap.AnimationTurnOnFlipTrigger();
    }

    private void AnimationActionTrigger()
    {
        weap.AnimationActionTrigger();
    }
}
