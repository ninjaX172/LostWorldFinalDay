using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterSpell : MonoBehaviour
{
    // Start is called before the first frame update
    public float coolDown;
    private float currentDuration;
    public GameObject aura;

    private void Start()
    {
        currentDuration = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if(currentDuration < coolDown)
        {
            currentDuration += 1 * Time.deltaTime;
        }
        else
        {
            GameObject u = Instantiate(aura, transform.position, Quaternion.Euler(-90, 0f, 0f));
            u.transform.parent = transform;
            u.transform.position = new Vector3(u.transform.position.x, u.transform.position.y, -2.92f);


            currentDuration = 0;

        }
    }
}
