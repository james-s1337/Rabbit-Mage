using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAggressiveWeaponData", menuName = "Data/Weapon Data/Aggressive Weapon")]
public class SO_AggressiveWeaponData : SO_WeaponData
{
    [SerializeField] private WeaponAttackDetails[] attackDetails;
    public WeaponAttackDetails[] AttackDetails { get => attackDetails; set => attackDetails = value; }

    [SerializeField] private SkillType skillType;
    public SkillType SkillType { get => skillType; }

    [SerializeField] private WeaponSpell weapSpell;
    public WeaponSpell WeapSpell { get => weapSpell; }

    [SerializeField] private WeaponAttackDetails[] comboDetails;
    public WeaponAttackDetails[] ComboDetails { get => comboDetails; set => comboDetails = value; }

    private void OnEnable()
    {
        amountOfAttacks = attackDetails.Length;
        movementSpeeds = new float[amountOfAttacks];

        for (int i = 0; i < amountOfAttacks; i++ )
        {
            movementSpeeds[i] = attackDetails[i].movementSpeed;
        }

        comboAttacks = comboDetails.Length;
        comboMovementSpeeds = new float[comboAttacks];

        for (int i = 0; i < comboAttacks; i++)
        {
            comboMovementSpeeds[i] = comboDetails[i].movementSpeed;
        }
    }
}

public enum SkillType
{
    Spell,
    Combo
}
