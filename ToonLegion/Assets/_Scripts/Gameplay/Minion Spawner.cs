using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public float cooldown;
    private float temp;
    public GameObject skeleton;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(temp < cooldown)
        {
            temp = temp + 1 *Time.deltaTime;

        }
        else
        {
            temp = 0;
            Instantiate(skeleton, transform.position, Quaternion.identity);
        }
    }
}
