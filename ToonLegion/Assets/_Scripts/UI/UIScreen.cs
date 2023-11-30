using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour
{
    public ScreenType screenType;
    public Canvas canvas;
    
    public bool IsOpen
    {
        get => canvas.enabled;
        private set => canvas.enabled = value;
    }
    
    public virtual void Open()
    {
        if (!canvas.enabled)
        {
            canvas.enabled = true;
        }
    }
    
    public void Close()
    {
        if (canvas.enabled)
        {
            canvas.enabled = false;
        }
    }
}
