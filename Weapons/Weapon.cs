using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected SO_WeaponData weapData;

    protected Animator weapAnimator;
    protected Animator playerAttackAnimator;

    protected PlayerAttackState state;
    protected PlayerSkillState skillState;
    protected int attackNum;

    protected int skillNum;

    protected bool isSkill;

    protected virtual void Awake()
    {
        weapAnimator = transform.Find("Weapon").GetComponent<Animator>();
        playerAttackAnimator = transform.Find("AttackPlayer").GetComponent<Animator>();

        attackNum = 0;
        skillNum = 0;

        gameObject.SetActive(false);
    }

    public virtual void EnterSkill()
    {
        isSkill = true;

        gameObject.SetActive(true);

        weapAnimator.SetInteger("attackCounter", -1);
        playerAttackAnimator.SetInteger("attackCounter", -1);

        weapAnimator.SetInteger("skillCounter", skillNum);
        playerAttackAnimator.SetInteger(weapData.ComboName + "Num", skillNum);

        playerAttackAnimator.SetBool("attack", true);
        weapAnimator.SetBool("attack", true);

        playerAttackAnimator.SetBool(weapData.ComboName, true);
        weapAnimator.SetBool("skill", true);
    }

    public virtual void ContinueSkill()
    {
        skillNum++;
        if (skillNum >= weapData.comboAttacks)
        {
            ExitSkill();
        }

        weapAnimator.SetInteger("skillCounter", skillNum);
        playerAttackAnimator.SetInteger(weapData.ComboName + "Num", skillNum);
    }

    public virtual void ExitSkill()
    {
        gameObject.SetActive(false);

        isSkill = false;

        playerAttackAnimator.SetBool("attack", false);
        weapAnimator.SetBool("attack", false);

        playerAttackAnimator.SetBool(weapData.ComboName, false);
        weapAnimator.SetBool("skill", false);

        skillNum = 0;

        skillState.FinishCombo();
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        weapAnimator.SetInteger("attackCounter", attackNum);
        playerAttackAnimator.SetInteger("attackCounter", attackNum);

        playerAttackAnimator.SetBool("attack", true);
        weapAnimator.SetBool("attack", true);
    }

    public virtual void ExitWeapon()
    {
        playerAttackAnimator.SetBool("attack", false);
        weapAnimator.SetBool("attack", false);  

        gameObject.SetActive(false);

        attackNum++;
        if (attackNum >= weapData.amountOfAttacks)
        {
            attackNum = 0;
        }
    }

    #region Animation Triggers

    public virtual void AnimationFinishTrigger()
    {
        if (!isSkill)
        {
            state.AnimationFinishTrigger();
        }
        else
        {
            skillState.AnimationFinishTrigger();
        }
    }

    public virtual void AnimationStartMovementTrigger()
    {
        if (!isSkill)
        {
            state.SetPlayerVelocity(weapData.movementSpeeds[attackNum]);
        }
        else
        {
            skillState.SetPlayerVelocity(weapData.comboMovementSpeeds[skillNum]);
        }
    }

    public virtual void AnimationStopMovementTrigger()
    {
        if (!isSkill)
        {
            state.SetPlayerVelocity(0);
        }
        else
        {
            skillState.SetPlayerVelocity(0);
        }
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        if (!isSkill)
        {
            state.SetFlipCheck(false);
        }
        else
        {
            skillState.SetFlipCheck(false);
        }
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        if (!isSkill)
        {
            state.SetFlipCheck(true);
        }
        else
        {
            skillState.SetFlipCheck(true);
        }
    }

    public virtual void AnimationActionTrigger()
    {

    }
    #endregion

    public void InitializeWeap(PlayerAttackState state)
    {
        this.state = state;
    }

    public void InitializeWeapSkill(PlayerSkillState skillState)
    {
        this.skillState = skillState;
    }

    public bool IsShield()
    {
        return weapData.GetType() == typeof(SO_DefensiveWeapon);
    }
}
