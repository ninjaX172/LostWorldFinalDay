using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    public float destroyAfterSeconds;
    public PlayerStats ps;

    //Current stats;
    public float currentDamage;
    public float currentSpeed;
    public float currentCooldownDuration;
    public int currentPierce;



    private void Awake()
    {
        ps = FindObjectOfType<PlayerStats>();

        currentDamage = weaponData.Damage + ps.currentMight;
        currentSpeed = weaponData.Speed;
        currentCooldownDuration = weaponData.CoolDownDuration;
        currentPierce = weaponData.Pierce;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DirectionChecker(Vector3 dir)
    {
        direction = dir;
        float dirx = direction.x;
        float diry = direction.y;
        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        if (dirx < 0 && diry == 0)
        {

            scale.x = scale.x * -1;
            scale.y = scale.y * -1;

        } //going left
        else if (dirx > 0 && diry == 0) // right
        {
            scale.x = scale.x * 1;
            scale.y = scale.y * 1;
        }
        else if (dirx == 0 && diry > 0) // up
        {
            scale.x = scale.x * -1;
        }
        else if (dirx == 0 && diry < 0) //down
        {
            scale.y = scale.y * -1;
        }
        else if (dirx > 0 && diry > 0) //up right
        {
            rotation.z = 0f;
        }
        else if (dirx > 0 && diry < 0) // down right
        {
            rotation.z = -90f;
        }
        else if (dirx < 0 && diry > 0) // up left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = -90f;
        }
        else if (dir.x < 0 && dir.y < 0) // down left
        {
            scale.x = scale.x * -1;
            scale.y = scale.y * -1;
            rotation.z = 0f;
        }

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);

    }


    public void AllDirection(Vector3 dir, Vector3 rot)
    {
        direction = dir;
        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;
        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rot);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {


        
        if (collision.CompareTag("Player"))
        {
            PlayerStats e = collision.GetComponent<PlayerStats>();
            
            e.takeDamage(currentDamage);
            ReducePierce();
        }


        if (currentPierce <= 0) //Destroy since it can no longer damage enemy
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Counter"))
        {
            Destroy(gameObject);
        }
    }

    void ReducePierce()
    {
        currentPierce = currentPierce - 1;
    }

}
