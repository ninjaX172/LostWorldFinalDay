using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BuffScriptableObject", menuName = "ScriptableObjects/Buff")]

public class BuffScriptableObject : ScriptableObject
{
    [SerializeField]
    GameObject prefab;
    public GameObject Prefab { get => prefab; private set => prefab = value; }
    //base state for weapons
    [SerializeField]
    float damageBuff;
    public float DamageBuff { get => damageBuff; private set => damageBuff = value; }

    [SerializeField]
    float speedBuff;
    public float SpeedBuff { get => speedBuff; private set => speedBuff = value; }

    [SerializeField]
    float healthBuff;
    public float HealthBuff { get => healthBuff; private set => healthBuff = value; }

    [SerializeField]
    float cooldown;
    public float Cooldown { get => cooldown; private set => cooldown = value; }

}
