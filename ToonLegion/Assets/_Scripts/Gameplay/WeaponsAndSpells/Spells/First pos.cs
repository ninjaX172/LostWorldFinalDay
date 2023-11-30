using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firstpos : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mousepos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public float damage;
    public float angle;


    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousepos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction towards the mouse cursor without normalizing
        Vector2 directionToMouse = mousepos - transform.position;

        // Rotate the direction vector by 45 degrees
        float angleInRadians = angle * Mathf.Deg2Rad; // Convert angle to radians
        Vector2 direction = new Vector2(
            directionToMouse.x * Mathf.Cos(angleInRadians) - directionToMouse.y * Mathf.Sin(angleInRadians),
            directionToMouse.x * Mathf.Sin(angleInRadians) + directionToMouse.y * Mathf.Cos(angleInRadians)
        );

        rb.velocity = direction.normalized * force;
        float rot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
        Destroy(gameObject, 4f);
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
        if (collision.CompareTag("HellHound"))
        {
            HellHoundStats hs = collision.GetComponent<HellHoundStats>();
            hs.TakeDamage(damage);
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
        if (collision.CompareTag("FlyingMonster"))
        {
            FlyingMonsterstats em = collision.GetComponent<FlyingMonsterstats>();

            em.TakeDamage(damage);
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
