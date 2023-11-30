using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStats : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject demonBoss;
    GameObject scriptBObject;
    private bool Once;
    //private void OnDestroy()
    //{
    //    Instantiate(demonBoss, transform.position, Quaternion.identity);
    //}
    private void Start()
    {
        Once = false;
        scriptBObject = GameObject.Find("Hero");
    }
    private void Update()
    {
        if(scriptBObject.GetComponent<EnemyStats>().currHealth <= 25 && Once == false)
        {
            Instantiate(demonBoss, transform.position, Quaternion.identity);
            Once = true;
        }
    }
}
