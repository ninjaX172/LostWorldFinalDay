using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    // Start is called before the first frame update
    Transform player;
    Transform parentTransform;
    public GameObject bullet;
    private float shotCooldown;
    public float startShotCooldown;
    public float distanceToShoot;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;
        shotCooldown = startShotCooldown;
        parentTransform = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        transform.up = direction;
        if(shotCooldown <= 0 && Vector2.Distance(player.position, parentTransform.position) <= distanceToShoot)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            shotCooldown = startShotCooldown;
        }
        else
        {
            shotCooldown -= Time.deltaTime;
        }

    }
}
