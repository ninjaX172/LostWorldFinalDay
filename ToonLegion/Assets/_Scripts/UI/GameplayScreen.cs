using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScreen: UIScreen
{
    //private List<Image> hearts = new List<Image>();
    //public Sprite emptyHeartSprite;
    //public Sprite fullHeartSprite;


    //void Start()
    //{

    //    if (canvas == null)
    //    {
    //        Debug.LogError("Canvas is null!");
    //    }
    //    for (int i = 1; i < canvas.transform.Find("Hearts").childCount + 1; i++)
    //    {
    //        var temp = canvas.transform.Find($"Hearts/Heart{i}").GetComponent<Image>();
    //        temp.enabled = false;
    //        hearts.Add(temp);
    //        Debug.Log("Hello from Gameplay Screen");
    //    }
       
         

    //}

    //public void OnEnable()
    //{
    //    EventManager.Instance.PlayerHealthChanged += EventManagerOnPlayerHealthChanged;

    //}


    //public void OnDisable()
    //{
    //    EventManager.Instance.PlayerHealthChanged -= EventManagerOnPlayerHealthChanged;

    //}

    //private void EventManagerOnPlayerHealthChanged(int currentHealth, int maxHealth)
    //{
    //    for(int i = 0; i < hearts.Count; i++)
    //    {
    //        if (i < currentHealth)
    //        {
    //            hearts[i].sprite = fullHeartSprite;
    //        }
    //        else
    //        {
    //            hearts[i].sprite = emptyHeartSprite;
    //        }

    //        if (i < maxHealth)
    //        {
    //            hearts[i].enabled = true;
    //        }
    //        else
    //        {
    //            hearts[i].enabled = false;
    //        }
    //    }
    //}
    
   
}
