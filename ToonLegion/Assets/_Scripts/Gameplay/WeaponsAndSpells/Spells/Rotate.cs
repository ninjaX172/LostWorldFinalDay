using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Update()
    {
       transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z + 0.5f);
    }
}
