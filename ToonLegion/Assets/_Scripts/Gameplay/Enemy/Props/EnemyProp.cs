using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyProp", menuName = "ScriptableObjects/Prop")]
public class EnemyProp : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField]
    float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }



}
