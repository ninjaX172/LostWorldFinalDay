using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSkeletonKill : MonoBehaviour
{
    // Start is called before the first frame update
    //[System.Serializable]
    //public class Drops
    //{
    //    public float dropRate;
    //    public string name;
    //    public GameObject prefab;
    //}

    public float dropRateIfPlayerHaveNecroAblility;
    public GameObject prefab;
    private GameObject necro;

    

    

    private void OnDestroy()
    {

        if(GameObject.Find("Player 1 1(Clone)") == null)
        {
            return;
        }
        
        necro = GameObject.Find("Player 1 1(Clone)").transform.Find("Necro Ability").gameObject;



        if (!gameObject.scene.isLoaded)
        {
            return;
        }

        if (!necro.activeSelf)
        {
            

            return;
        }
        

        float dropRate = UnityEngine.Random.Range(0f, 100f);
        if(dropRateIfPlayerHaveNecroAblility >= dropRate)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }


        
    }



}
