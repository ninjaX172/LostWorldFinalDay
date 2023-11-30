using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyScriptableObject em;
    Transform player;
    //public float moveSpeed;
    private SpriteRenderer sr;
    public float DistanceToStop;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        {
            return;
        }
         //move from current position to player
        Vector2 direction = (player.position - transform.position).normalized; //Get the distance via x, y of the distance between player and enemey.
        if(Vector2.Distance(player.position, transform.position) >= DistanceToStop)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, em.MoveSpeed * Time.deltaTime);
        }
        

        //print(direction);
        if (direction.x > 0)
        {
            sr.flipX = true;
        }
        else if (direction.x < 0)
        {
            sr.flipX = false;
        }

    }
}
