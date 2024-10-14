using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyAttackDetails
{
    public string attackName;
    public int[] attackDamages; // Corresponds to attackNum
    public float[] movementSpeeds; // Corresponds to attackNum
    public Vector2[] movementAngles; // Corresponds to attackNum
    public float cooldown; // How long it will take for the next attack to be available
}
