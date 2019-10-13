# IExtraFuncionts

This script is a namespace.
The reason why I choose to make it a namespace is because, I don't want to inherit the whole script
and because the function's will be called a lot when they are in use. 

## Wait(action,_timeToWait)

```csharp
static public WaitTimer _waitTimer;
    public static void Wait(float secondsToWait, Action action)
    {
        _waitTimer = new WaitTimer();
        _waitTimer.StartCoroutine(_wait(action, secondsToWait));
    }

    static IEnumerator _wait(Action action, float secondsToWait)
    {
        yield return new WaitForSecondsRealtime(secondsToWait);
        action();
    }
```

## Map()

``` csharp
public static float Map(float x, float in_min, float in_max, float out_min, float out_max) => (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
```