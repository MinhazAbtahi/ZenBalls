using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Fields
    private static T instance;
    #endregion

    #region Properties
    public static T Instance
    {
        get
        {
            //if (instance == null)
            //{
            //    instance = FindObjectOfType<T>();
            //    if (instance == null)
            //    {
            //        GameObject obj = new GameObject();
            //        obj.name = typeof(T).Name;
            //        instance = obj.AddComponent<T>();
            //    }
            //}
            return instance;
        }
    }
    #endregion

    #region Monobehaviour Callbacks
    /// Use this for initialization.
    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;           
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        instance = null;
    }
    #endregion
}