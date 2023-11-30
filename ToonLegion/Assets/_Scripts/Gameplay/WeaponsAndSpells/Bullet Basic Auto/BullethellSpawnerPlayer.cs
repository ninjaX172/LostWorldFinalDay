using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullethellSpawnerPlayer : MonoBehaviour
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
        if (collision.CompareTag("Enemy"))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();
            em.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Skeleton Skull"))
        {
            RangeEnemyStats em = collision.GetComponent<RangeEnemyStats>();

            em.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("Socrerer"))
        {
            NecroStats em = collision.GetComponent<NecroStats>();

            em.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("PillarDestroy"))
        {
            PillarStats em = collision.GetComponent<PillarStats>();

            em.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("BringerOfDeath"))
        {
            BringerStats em = collision.GetComponent<BringerStats>();

            em.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("Warlock"))
        {
            CorruptedStats em = collision.GetComponent<CorruptedStats>();

            em.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("FlyingMonster"))
        {
            FlyingMonsterstats em = collision.GetComponent<FlyingMonsterstats>();

            em.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("HellHound"))
        {
            HellHoundStats em = collision.GetComponent<HellHoundStats>();

            em.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("EyeSpawner"))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();

            em.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("SmallEye"))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();

            em.TakeDamage(bulletDamage);
            Destroy(gameObject);

        }

    }
}
