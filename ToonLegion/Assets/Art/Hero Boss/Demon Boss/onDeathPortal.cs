using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDeathPortal : MonoBehaviour
{
    private void OnDestroy()
    {
        var position = gameObject.transform.position;

        EventManager.Instance.OnBossDeath("Demon",new Vector2Int((int)position.x, (int)position.y));

    }
}
