using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Vitals")]
    public int health;
   
    [Header("Combat Data")]
    public float attack;
    public float pDefense; // Physical defense
    public float mDefense; // Magic defense

    public EnemyAttackDetails[] attacks; // attackNum corresponds to the single animation for the attack

    [Header("Movement")]
    public bool canJump;
    public bool isFlying;
    public bool isAnchored;

    public float jumpPower;
    public float movementSpeed;
}
