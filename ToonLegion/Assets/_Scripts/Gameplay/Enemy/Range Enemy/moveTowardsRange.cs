using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTowardRange : MonoBehaviour
{
    // Start is called before the first frame update
    bool isMoving = false;
    public EnemyScriptableObject em;
    Transform player;
    //public float moveSpeed;
    private SpriteRenderer sr;

    public float DistanceToStop;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        sr = gameObject.GetComponentInParent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isMoving = true;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            //transform.parent.position = Vector2.MoveTowards(transform.parent.position, player.transform.position, em.MoveSpeed * Time.deltaTime); //move from current position to player
            Vector2 direction = (player.position - transform.parent.position).normalized; //Get the distance via x, y of the distance between player and enemey.

            if (Vector2.Distance(player.position, transform.position) >= DistanceToStop)
            {
                transform.parent.position = Vector2.MoveTowards(transform.parent.position, player.transform.position, em.MoveSpeed * Time.deltaTime); //move from current position to player
            }

            if (direction.x > 0)
            {
                sr.flipX = false;
            }
            else if (direction.x < 0)
            {
                sr.flipX = true;
            }
        }
    }
}