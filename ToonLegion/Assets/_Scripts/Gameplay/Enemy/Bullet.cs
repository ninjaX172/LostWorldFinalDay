using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public EnemyScriptableObject range;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3f);
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Player"))
        {
            PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
            
            ps.takeDamage(range.Damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Counter"))
        {
            Destroy(gameObject);
        }
    }

}
