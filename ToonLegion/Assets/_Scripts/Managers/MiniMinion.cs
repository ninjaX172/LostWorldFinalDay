
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MiniMinion : MonoBehaviour
{
    public float speed = 1.5f;
    public int minSize = 5;
    public int maxSize = 7;

    public int minRandomWalkTime = 4;
    public int maxRandomWalkTime = 9;

    private float currentWalkTime;

    private Tilemap tilemap;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector2 currentDirection = Vector2.zero;
    private Vector2 previousPosition = Vector2.zero;


    void Start()
    {
        currentWalkTime = 0;
        previousPosition = transform.position;
        tilemap = GameObject.Find("Walls").GetComponent<Tilemap>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        setRandomSize();
    }

    void Update()
    {
        Wander();
    }

    private void Wander()
    {
        if (currentWalkTime <= 0)
        {
            currentDirection = Direction2D.GetRandomDirection();
            float randomTime = Random.Range(minRandomWalkTime, maxRandomWalkTime);
            currentWalkTime = randomTime;
        }

        isCollidingWithAnotherMinion(transform.position);

        Vector2 targetPosition = (Vector2)transform.position + currentDirection * speed * Time.deltaTime;
        bool hasMove = Vector2.Distance(previousPosition, targetPosition) > 0.01f;
        if (!isCollidingWithTilemap(targetPosition) && hasMove && !isCollidingWithAnotherMinion(targetPosition))
        {
            transform.position = targetPosition;
            // Debug.Log("New Position" + targetPosition);
        }
        else
        {
            currentDirection *= -1;
            float randomTime = Random.Range(minRandomWalkTime, maxRandomWalkTime);
            // currentWalkTime = randomTime;
        }

        if (currentDirection.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (currentDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }

        // Debug.Log("Current walk time: " + currentWalkTime + "Current position" + targetPosition);
        currentWalkTime -= Time.deltaTime;
        previousPosition = transform.position;

    }

    bool isCollidingWithTilemap(Vector2 targetPosition)
    {
        foreach (var direction in Direction2D.cardinalDirections)
        {
            if (tilemap.GetTile(tilemap.WorldToCell(targetPosition + direction * 2)) != null)
            {
                return true;
            }
        }

        return false;
    }

    bool isCollidingWithAnotherMinion(Vector2 targetPosition)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(targetPosition, 0.2f);
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                if (collider.gameObject.name == "MiniMinion(Clone)")
                {
                    print(1);
                    return true;
                }
            }
        }

        return false;
    }

    void setRandomSize()
    {
        var randomScale = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomScale, randomScale, 1f);
    }


    public void TakeDamage(float damage)
    {

    }
}