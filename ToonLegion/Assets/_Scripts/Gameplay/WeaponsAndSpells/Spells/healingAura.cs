using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healingAura : MonoBehaviour
{
    // Start is called before the first frame update
    private float x = 0;
    PlayerStats ps;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Heal");
            x = x + 1 * Time.deltaTime;
            ps = collision.gameObject.GetComponent<PlayerStats>();
            if(ps.currentHealth >= 100)
            {
                return;
            }
            else
            {
                ps.currentHealth = ps.currentHealth + 5 * Time.deltaTime;
            }
        }
    }

}
