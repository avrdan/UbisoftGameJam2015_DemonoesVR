using UnityEngine;



public class SingletonMonoPersistent<T>: MonoBehaviour where T : SingletonMonoPersistent<T>
{
    static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Object.FindObjectOfType<T>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != instance)
            {
                Destroy(gameObject);
            }
        }
    }
    public static bool InstanceExists
    {
        get
        {
            return instance != null;
        }
    }
}

public abstract class SingletonMonoPrefab<T> : MonoBehaviour where T : SingletonMonoPrefab<T>
{
    protected static string Path()
    {
        return InvokeStaticMethod("Path", null) as string;
    }
    protected static object InvokeStaticMethod(string methodName, Object[] args)
    {
        return typeof(T).InvokeMember(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.InvokeMethod, null, null, args);
    }
    static T instance = null;
    protected abstract void OnCreation();

    public static T Instance
    {
        get
        {
            
            if (instance == null)
            {
                instance = ((GameObject)MonoBehaviour.Instantiate(Resources.Load(Path()))).GetComponent<T>();
                instance.OnCreation();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }
    public static bool InstanceExists
    {
        get
        {
            return instance != null;
        }
    }
}
public class SingletonMono<T> : MonoBehaviour where T : SingletonMono<T>
{
    static T instance = null;
    public static bool InstanceExists
    {
        get
        {
            return instance != null;
        }
    }
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Object.FindObjectOfType<T>();
                
            }
            return instance;
        }
    }

    
}
