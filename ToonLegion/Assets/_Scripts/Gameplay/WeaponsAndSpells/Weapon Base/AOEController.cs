using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEController : MonoBehaviour
{
    // Start is called before the first frame update
    public BuffScriptableObject aoedata;
    float currentCooldown;

    protected virtual void Start()
    {
        currentCooldown = aoedata.Cooldown; // at start current = cooldownduration
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = aoedata.Cooldown;
    }
}
