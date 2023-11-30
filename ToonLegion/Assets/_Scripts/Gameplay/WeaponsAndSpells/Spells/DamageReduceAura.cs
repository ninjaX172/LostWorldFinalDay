using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReduceAura : MonoBehaviour
{
    private Camera cam;
    Vector3 mousePos;
    public GameObject bullet;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring;
    public float radius; // Add a radius for the circle around the player

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotz);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && canFire)
        {
            canFire = false;

            // Calculate the direction from the player to the mouse position
            Vector3 direction = (mousePos - transform.position).normalized;

            // Calculate the spawn position within the circle radius around the player
            Vector3 spawnPosition = transform.position + direction * radius;

            // Instantiate the bullet at the spawn position
            Instantiate(bullet, spawnPosition, bullet.transform.rotation);
        }
    }
}
