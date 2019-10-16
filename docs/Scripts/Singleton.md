# Singleton

What does the T stand for?

### Description
Any class that inheritress from this class will become a singleton.
::: tip
#### Tip
Don't know what a singleton is? go to the How to use section
:::
### Parameters
**T** = Reference to the sub class
### Return
::: danger
#### WARNING
This will not return anything.Should only be inherited from.
:::


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