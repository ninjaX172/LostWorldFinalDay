using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDeathNightBorne : MonoBehaviour
{
    private void OnDestroy()
    {
        var position = gameObject.transform.position;

        EventManager.Instance.OnBossDeath("NightBorne_0", new Vector2Int((int)position.x, (int)position.y));

    }
}
