# IExtraFuncionts

This script is a namespace.
The reason why I choose to make it a namespace is because, I don't want to inherit the whole script
and because the function's will be called a lot when they are in use.

You can find the source script [here](https://github.com/ertugrul013/AllForOne/blob/master/Assets/Scripts/Libarys/IExtraFunctions.cs)

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

### How it works

This functions return a float.
This float contains the mapped variable between the ranges <br>


x = the variable that is going to be mapped <br>
in_min = is the minium that x will be <br>
in_max = is the maximum that x will be <br>
out_min = is the minium that the returned variable is going to be <br>
out_max = is the maximum that the returned variable is going to be <br>

``` csharp
public static float Map(float x, float in_min, float in_max, float out_min, float out_max) 
{
    return ((x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min);
}
```