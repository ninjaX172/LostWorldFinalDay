using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycloneMovment : MonoBehaviour
{
    // Start is called before the first frame update
    BringerStats em;
    public BossScriptableObjects boss;
    Transform player;
    //public float moveSpeed;
    private SpriteRenderer sr;
    void Start()
    {
        em = gameObject.GetComponent<BringerStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
   
    // Update is called once per frame
    void Update()
    {

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, boss.MoveSpeed * Time.deltaTime); //move from current position to player
        Vector2 direction = (player.position - transform.position).normalized; //Get the distance via x, y of the distance between player and enemey.
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
