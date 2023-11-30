using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDeathGolem : MonoBehaviour
{
    private void OnDestroy()
    {
        var position = gameObject.transform.position;

        EventManager.Instance.OnBossDeath("Old_Golem_walk_0", new Vector2Int((int)position.x, (int)position.y));

    }
}
