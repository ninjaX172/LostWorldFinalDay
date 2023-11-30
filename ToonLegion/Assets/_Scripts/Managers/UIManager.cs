using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIManager : Singleton<UIManager>
{
    private List<UIScreen> Screens;
    public UIScreen CurrentScreen { get; private set; }
    
    private void Awake()
    //public override void Awake()
    {
        //base.Awake();
        Screens = new List<UIScreen>(GetComponentsInChildren<UIScreen>());
    }

    public void ShowScreen(ScreenType targetScreen)
    {
        foreach (UIScreen screen in Screens)
        {
            if (screen.screenType == targetScreen)
            {
                
                screen.Open();
                CurrentScreen = screen;
            }
            else
            {
                screen.Close();
            }
        }
    }
}
