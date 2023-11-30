using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
// {
//     public static T Instance { get; private set; }
//     protected virtual void Awake() => Instance = this as T;
//
//     protected virtual void OnApplicationQuit()
//     {
//         Instance = null;
//         Destroy(gameObject);
//     }
// }
//
// public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
// {
//     protected override void Awake()
//     {
//         if (Instance != null) Destroy(gameObject);
//         base.Awake();
//     }
// }
//
// public abstract class PersistentSingleon<T> : Singleton<T> where T : MonoBehaviour
// {
//     protected override void Awake()
//     {
//         base.Awake();
//         DontDestroyOnLoad(gameObject);
//     }
// }


public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    SetupInstance();
                }
            }
            return instance;
        }
    }
    public virtual void Awake()
    {
        RemoveDuplicates();
    }
    private static void SetupInstance()
    {
        instance = (T)FindObjectOfType(typeof(T));
        if (instance == null)
        {
            GameObject gameObj = new GameObject();
            gameObj.name = typeof(T).Name;
            instance = gameObj.AddComponent<T>();
            // DontDestroyOnLoad(gameObj);
        }
    }
    private void RemoveDuplicates()
    {
        if (instance == null)
        {
            instance = GetComponent<T>();
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}