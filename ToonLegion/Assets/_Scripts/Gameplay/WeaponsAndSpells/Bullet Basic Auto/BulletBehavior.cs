using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mousepos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public float damage;
    PlayerStats ps;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousepos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousepos - transform.position;
        Vector3 rotation = transform.position - mousepos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        Destroy(gameObject, 4f);
        ps = GameObject.FindAnyObjectByType<PlayerStats>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Walls"))
        {
            Destroy(gameObject);
        }


        if (collision.CompareTag("Enemy"))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();
            em.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Skeleton Skull"))
        {
            RangeEnemyStats em = collision.GetComponent<RangeEnemyStats>();

            em.TakeDamage(damage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("Socrerer"))
        {
            NecroStats em = collision.GetComponent<NecroStats>();

            em.TakeDamage(damage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("PillarDestroy"))
        {
            PillarStats em = collision.GetComponent<PillarStats>();

            em.TakeDamage(damage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("BringerOfDeath"))
        {
            BringerStats em = collision.GetComponent<BringerStats>();

            em.TakeDamage(damage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("Warlock"))
        {
            CorruptedStats em = collision.GetComponent<CorruptedStats>();

            em.TakeDamage(damage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("FlyingMonster"))
        {
            FlyingMonsterstats em = collision.GetComponent<FlyingMonsterstats>();

            em.TakeDamage(damage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("HellHound"))
        {
            HellHoundStats em = collision.GetComponent<HellHoundStats>();

            em.TakeDamage(damage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("EyeSpawner"))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();

            em.TakeDamage(damage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("SmallEye"))
        {
            EnemyStats em = collision.GetComponent<EnemyStats>();

            em.TakeDamage(damage);
            Destroy(gameObject);

        }
        if (collision.CompareTag("KingSlime"))
        {
            SlimeStats ss = collision.GetComponent<SlimeStats>();
            ss.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (collision.CompareTag("Grim"))
        {
            GrimStats ss = collision.GetComponent<GrimStats>();
            ss.TakeDamage(damage);
            Destroy(gameObject);
        }

    }

}
