using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Perk/Awareness Perk")]
public class AwarenessPerk: BasePerk
{
    
    public override void ApplyPerk()
    {
        //Increase damage to skeletons, incease movement speed to player

        // Find all the enemies in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Iterate over each enemy
        foreach (GameObject enemy in enemies)
        {
            // Get the enemy's health component
            EnemyStats enemyHealth = enemy.GetComponent<EnemyStats>();

            
            if (enemyHealth != null)
            {
                enemyHealth.currDamage += 2;
            }
        }
        Debug.Log("ERROR");
        PlayerStats x = GameObject.Find("Player 1 1(Clone)").GetComponent<PlayerStats>();
        x.currentMoveSpeed += 2;

    }


}