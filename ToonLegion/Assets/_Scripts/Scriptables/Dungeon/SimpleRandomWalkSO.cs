using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/SimpleRandomWalkSO")]
public class SimpleRandomWalkSO : ScriptableObject
{
    public int iterations = 10;
    public int walkLength=10;
    public bool startRandomlyEachIteration = true;
    public bool useRandomSeed=true;
    public int seed=0;
}
