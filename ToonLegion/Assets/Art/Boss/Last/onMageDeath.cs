using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onMageDeath : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDestroy()
    {
        var position = gameObject.transform.position;

        EventManager.Instance.OnBossDeath("mage_guardian-magenta_0", new Vector2Int((int)position.x, (int)position.y));

    }
}
