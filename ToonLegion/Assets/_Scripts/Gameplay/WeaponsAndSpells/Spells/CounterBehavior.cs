using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration;

    private void Start()
    {
        Destroy(gameObject, duration);
    }



}
