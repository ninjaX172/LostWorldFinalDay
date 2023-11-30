using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRateManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    [System.Serializable]
    public class Drops
    {
        public float dropRate;
        public string name;
        public GameObject prefab;
    }

    public List<Drops> d;
    // Update is called once per frame
    void OnDestroy()
    {
        if (!gameObject.scene.isLoaded)
        {
            return;
        }
        // Range from 1- 100 for drop chance if less than dropRate then it will drop if higher than false. //
        float dropRange = UnityEngine.Random.Range(0f, 100f);
        foreach(Drops i in d) // Will check list of Drops, calcuate if the index drop rate is lower than drop range. if is then spawn prefab on position of enemy. 
        {
            if(i.dropRate > dropRange)
            {
                Instantiate(i.prefab, transform.position, Quaternion.identity);
            }
        }
        
    }

    
    
    
}
