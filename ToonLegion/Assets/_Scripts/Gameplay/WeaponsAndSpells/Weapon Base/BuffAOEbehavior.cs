using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffAOEbehavior : MonoBehaviour
{
    // Start is called before the first frame update


    public BuffScriptableObject buff;
    public float destroyAfterSeconds;
    protected virtual void Start()
    {
        
        Destroy(gameObject, destroyAfterSeconds);
    }

    // Update is called once per frame
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();

        }
    }
}
