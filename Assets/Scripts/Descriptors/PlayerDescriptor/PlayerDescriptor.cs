using System;
using UnityEngine;

[Serializable]
public class PlayerDescriptor 
{
    [field: SerializeField] public float PlayerHealth;
    [field: SerializeField] public float MaxPlayerHealth;
    [field: SerializeField] public float PlayerLowHpBorder;
    [field: SerializeField] public float Strength;
    [field: SerializeField] public float MaxStrength;
}
