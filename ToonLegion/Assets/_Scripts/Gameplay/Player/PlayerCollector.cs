using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    // If player touch a collectable object do this... So 
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;
    private void Awake()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        playerCollector.radius = player.currentMagnet;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollect collectible))
        {
            //get the rigidboy component on the item

            //Vector2 pointing from the item to the player
            //Applies force
            if (collision.CompareTag("Red"))
            {
                //Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                //Vector2 forceDirection = (transform.position - collision.transform.position).normalized;
                //rb.AddForce(forceDirection * pullSpeed);
                collectible.MightIncrease();

            }
            else if (collision.CompareTag("Green"))
            {
                //Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                //Vector2 forceDirection = (transform.position - collision.transform.position).normalized;
                //rb.AddForce(forceDirection * pullSpeed);
                collectible.HealPlayerHealth();
            }
            else if (collision.CompareTag("Sliver"))
            {
                //Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                //Vector2 forceDirection = (transform.position - collision.transform.position).normalized;
                //rb.AddForce(forceDirection * pullSpeed);
                collectible.SpeedIncrease();
            }

        }


    }

    //In order to make gem towards player make it so that if detect a gem the gem velecoity will move to player.
}
