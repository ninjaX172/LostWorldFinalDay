using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SoulStealerAbility : MonoBehaviour
{
    private GameObject targetObject;
    private int numberOfSoulsAvailable = 0;
    private readonly int minimumSoulsRequired = 1;
    
    private Light2D light2D;
    private float lightIntensity = 4f;
    private void Start()
    {
        light2D = GetComponentInChildren<Light2D>();
    }

    private void Update()
    {
        if (numberOfSoulsAvailable >= minimumSoulsRequired)
        {
            light2D.intensity = lightIntensity;
        }
        else
        {
            light2D.intensity = 0;
        }
    }

    public bool attemptToUseAbility()
    {
        if (numberOfSoulsAvailable < minimumSoulsRequired)
        {
            return false;
        }
        numberOfSoulsAvailable = 0;
        return true;
        
    }

    void OnEnable()
    {
        EventManager.Instance.BossDeath += EventManager_OnBossDeath;
    }
    
    void OnDisable()
    {
        EventManager.Instance.BossDeath -= EventManager_OnBossDeath;
    }

    private void EventManager_OnBossDeath(string arg0, Vector2Int arg1)
    {
        numberOfSoulsAvailable++;
    }
}