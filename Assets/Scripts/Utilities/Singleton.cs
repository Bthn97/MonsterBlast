using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance = FindObjectOfType<T>();
                    if (_instance == null)
                    {
                        Debug.LogError("Singleton of type '" + typeof(T) + "' not found in the scene.");
                    }
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        lock (_lock)
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogError("There are more than 1 instance of Singleton of type '" + typeof(T) + "'. Keeping only one. Destroying the others.");
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this as T;
            }
        }
    }
}