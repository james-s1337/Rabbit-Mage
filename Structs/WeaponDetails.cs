using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeaponDetails
{
    
}

[System.Serializable]
public struct WeaponAttackDetails
{
    public string attackName;
    public float movementSpeed;
    public float pDamageAmount;
    public float mDamageAmount;
}

[System.Serializable]
public struct WeaponDefenseDetail
{
    public float damageNegation; // How much damage in percentage is blocked when shielding
    public float movementSpeed; // Player's movement speed while using shield (for charge attacks)
    public float initialStaminaDrain; // For shields that utilize a charge and knocks enemies back
    public float passiveStaminaDrain; // How much stamina is drained passively while shield is held down (so players cannot just hold it forever, they need to time it)
    public float hitStaminaDrain; // How much stamina is drained when player is hit while shielding
    public bool canParry; // Parries within 0.2 seconds of shield activating
}

/* 2 Base types of skill, which are: 
 * Spell Cast (spawns in a certain amount of projectiles)
 * Combo (certain effects can be attached to each hit via animation function)
 * */
[System.Serializable]
public struct WeaponSpell
{
    public int numOfProj; // Number of this spell instance that it will spawn
    public float spawnTime; // How long it takes to spawn each projectile
    public GameObject spellProj; // The spell object itself
    // The damage and behaviour will be found in the spell game object
}