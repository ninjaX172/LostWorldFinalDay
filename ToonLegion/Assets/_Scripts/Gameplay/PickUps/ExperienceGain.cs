using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGain : MonoBehaviour, ICollect
{
    private void Update()
    {
        Destroy(gameObject, 20);
    }


    public void MightIncrease()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.currentMight += 1;
    }

    public void HealPlayerHealth()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.currentHealth = Mathf.Clamp(player.currentHealth+1, 0, player.currentMaxHealth);
    }

    public void SpeedIncrease()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.currentMoveSpeed += 1;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
