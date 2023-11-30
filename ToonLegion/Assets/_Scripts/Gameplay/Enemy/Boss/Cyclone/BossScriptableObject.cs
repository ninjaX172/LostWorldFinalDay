using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BossScriptableObjects", menuName = "ScriptableObjects/Boss")]
public class BossScriptableObjects : ScriptableObject
{
    // Base Stats
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField]
    float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField]
    float damage;
    public float Damage { get => damage; private set => damage = value; }
    [SerializeField]
    int pillar;
    public int Pillar { get => pillar; private set => pillar = value; }

}
