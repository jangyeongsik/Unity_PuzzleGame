using System.Collections;
using System.Collections.Generic;

public class Singleton <T> where T : class, new()
{
    static private T instance;
    static public T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }
}
