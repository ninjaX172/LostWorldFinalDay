
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class onPlayerDeath : MonoBehaviour
{
    private void OnDestroy()
    {
        SceneManager.LoadScene("SampleScene");

    }
}
