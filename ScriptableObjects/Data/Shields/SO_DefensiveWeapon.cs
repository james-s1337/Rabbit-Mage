using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newDefensiveWeaponData", menuName = "Data/Weapon Data/Defensive Weapon")]
public class SO_DefensiveWeapon : SO_WeaponData
{
    [SerializeField] private WeaponDefenseDetail defenseDetail;

    public WeaponDefenseDetail DefenseDetail { get => defenseDetail; set => defenseDetail = value; }
    private void OnEnable()
    {
        amountOfAttacks = 1;

        movementSpeeds = new float[amountOfAttacks];
        movementSpeeds[0] = defenseDetail.movementSpeed;
    }
}
