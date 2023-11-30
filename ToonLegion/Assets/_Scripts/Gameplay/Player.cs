using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float moveSpeed = 5f;
    float speedX, speedY;
    Rigidbody2D rb;
    public GameObject spotLightPrefab;
    
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject spotLightInstance = Instantiate(spotLightPrefab, transform.position, Quaternion.identity);
        spotLightInstance.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        speedY = Input.GetAxisRaw("Vertical") * moveSpeed;
        rb.velocity = new Vector2(speedX, speedY);
        
        
    }
}
