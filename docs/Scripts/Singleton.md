# Singleton

## Code

``` csharp
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance = null;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = gameObject.GetComponent<T>();
            //Dont destroy on load won't be added
        }
        else if (instance.GetInstanceID() != GetInstanceID())
        {
            Destroy(gameObject);
        }
    }
}
```