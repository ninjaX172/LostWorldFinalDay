using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySkeletonBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject enemyToFollow;
    public EnemyScriptableObject x;
    private void Awake()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemyToFollow = collision.gameObject;
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, enemyToFollow.transform.position, x.MoveSpeed * Time.deltaTime);
        }
        if (collision.CompareTag("Skeleton Skull"))
        {
            enemyToFollow = collision.gameObject;
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, enemyToFollow.transform.position, x.MoveSpeed * Time.deltaTime);
        }
        if (collision.CompareTag("BringerOfDeath"))
        {
            enemyToFollow = collision.gameObject;
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, enemyToFollow.transform.position, x.MoveSpeed * Time.deltaTime);
        }
        if (collision.CompareTag("Socrerer"))
        {
            enemyToFollow = collision.gameObject;
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, enemyToFollow.transform.position, x.MoveSpeed * Time.deltaTime);
        }
        if (collision.CompareTag("Warlock"))
        {
            enemyToFollow = collision.gameObject;
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, enemyToFollow.transform.position, x.MoveSpeed * Time.deltaTime);
        }
        if (collision.CompareTag("FlyingMonster"))
        {
            enemyToFollow = collision.gameObject;
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, enemyToFollow.transform.position, x.MoveSpeed * Time.deltaTime);
        }
        if (collision.CompareTag("SmallEye"))
        {
            enemyToFollow = collision.gameObject;
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, enemyToFollow.transform.position, x.MoveSpeed * Time.deltaTime);
        }
        if (collision.CompareTag("HellHound"))
        {
            enemyToFollow = collision.gameObject;
            transform.parent.position = Vector2.MoveTowards(transform.parent.position, enemyToFollow.transform.position, x.MoveSpeed * Time.deltaTime);
        }
        
    }
}
