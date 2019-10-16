# IExtraFuncionts

## Wait(action,_timeToWait)

### Description
Handles the waiting inside of the game loop

### return
Action callback that releases the wait loop

### parameters
**secondsToWait** = The amount of seconds to wait real time <br> 
**action** = should be an empty action check the how to use section if you don't know how it works<br>

```csharp
static public WaitTimer _waitTimer;
    public static void Wait(Action action, float secondsToWait)
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
[source](https://github.com/ertugrul013/AllForOne/blob/master/Assets/Scripts/Libarys/IExtraFunctions.cs)

****

## Map()

### Description
Maps a given float to a new min and max range

### return
This functions return a float.
This float contains the mapped variable between the ranges <br>

### parameters
**x** = the variable that is going to be mapped <br>
**in_min** = is the minium that x will be <br>
**in_max** = is the maximum that x will be <br>
**out_min** = is the minium that the returned variable is going to be <br>
**out_max** = is the maximum that the returned variable is going to be <br>

``` csharp
public static float Map(float x, float in_min, float in_max, float out_min, float out_max) 
{
    return ((x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min);
}
```

[source](https://github.com/ertugrul013/AllForOne/blob/master/Assets/Scripts/Libarys/IExtraFunctions.cs)