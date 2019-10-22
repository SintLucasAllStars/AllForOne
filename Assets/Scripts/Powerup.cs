using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    public bool isComplete = false;
    public float duration;
    public float storedValue;

    private void Update()
    {
        Timer();
        Run();
    }

    public virtual void Run() { }
    private void Timer() {
        duration += 1 * Time.deltaTime;
        if (duration >= 5)
        {
            isComplete = true;
        }
    }
}