using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptedMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyScriptableObject em;
    public float distanceToPlayer;
    private SpriteRenderer sr;
    Transform player;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.position - transform.position).normalized; //Get the distance via x, y of the distance between player and enemey.
        if (Vector2.Distance(player.position, transform.position) >= distanceToPlayer)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, em.MoveSpeed * Time.deltaTime);
        }


        //print(direction);
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
