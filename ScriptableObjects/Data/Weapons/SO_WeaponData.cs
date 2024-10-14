using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SO_WeaponData : ScriptableObject
{
    public int amountOfAttacks { get; protected set; }
    public float[] movementSpeeds { get; protected set; }

    public int comboAttacks { get; protected set; }
    [SerializeField] private string comboName;
    public float[] comboMovementSpeeds { get; protected set; }
    public string ComboName { get => comboName; }
}
