using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public CharacterScriptableObject em;
    
    //public float moveSpeed;
    Rigidbody2D rb;

    public Vector2 moveDir;
    PlayerStats ps;

    public float lastHorizontalVector;
    public float lastVerticalVector;
    public GameObject prefabLightSource;
    
    public Vector2 lastMovedVector;

    void Awake()
    {
        ps = FindAnyObjectByType<PlayerStats>();

        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f);
        GameObject lightSourceInstant = Instantiate(prefabLightSource, transform.position, Quaternion.identity);
        lightSourceInstant.transform.SetParent(transform);
    }

    // Update is called once per frame
    void Update()
    {
        InputMovement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void InputMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if(moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector);
        }
        if(moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }


    }

    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * (em.MoveSpeed + ps.currentMoveSpeed), moveDir.y * (em.MoveSpeed + ps.currentMoveSpeed));
    }

}
