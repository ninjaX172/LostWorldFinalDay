// Bullet


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletHell : MonoBehaviour
{
    public float bulletLife = 1f;  // Defines how long before the bullet is destroyed
    public float rotation = 0f;
    public float speed = 1f;


    private Vector2 spawnPoint;
    private float timer = 0f;
    public float bulletDamage;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }


    // Update is called once per frame
    void Update()
    {
        if (timer > bulletLife) Destroy(this.gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }


    private Vector2 Movement(float timer)
    {
        // Moves right according to the bullet's rotation
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerStats ps = collision.gameObject.GetComponent<PlayerStats>();
            ps.takeDamage(bulletDamage);
        }
        if (collision.CompareTag("Ally"))
        {
            AllySkeleton ps = collision.gameObject.GetComponent<AllySkeleton>();
            ps.TakeDamage(bulletDamage);
        }
        if (collision.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Counter"))
        {
            Destroy(gameObject);
        }

    }
}
