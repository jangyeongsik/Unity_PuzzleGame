using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton <T> : MonoBehaviour  where T : class 
{
    static private T instance;
    static public T Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
            this.Init();
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    protected virtual void Init()
    {

    }

}
