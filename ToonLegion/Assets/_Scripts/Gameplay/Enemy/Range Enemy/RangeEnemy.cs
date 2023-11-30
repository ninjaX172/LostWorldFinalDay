using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public EnemyScriptableObject em;
    Transform player;
    //public float moveSpeed;
    private SpriteRenderer sr;
    public float distanceToShoot;
    public float distanceToStop;


    public float fireRate;
    private float timeToFire;
    public Transform firingPoint;

    public float rotateSpeed = 0.0025f;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        sr = gameObject.GetComponent<SpriteRenderer>();
        timeToFire = 0f;
    }

    private void shoot() { 
        if(timeToFire <= 0f)
        {

            Debug.Log("Shoot");
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }

    
    
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(player.position, transform.position) <= distanceToShoot)
        {
            shoot();
        }


        Vector2 direction = (player.position - transform.position).normalized; //Get the distance via x, y of the distance between player and enemey.

        //If distance betwen them is less than 3f stop and shoot. Can shoot while moving. But stopping at a certain distance will prevent them from running to the player only melee enemy can do that.
        
        if(Vector2.Distance(player.position, transform.position) >= distanceToStop)
        {
           transform.position = Vector2.MoveTowards(transform.position, player.transform.position, em.MoveSpeed * Time.deltaTime); //move from current position to player
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
