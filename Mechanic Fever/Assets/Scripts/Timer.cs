using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Timer
{
    private const int MaxTimer = 10;

    private float timer;
    private bool frozen = false;

    public void Initialize()
    {
        GameManager.instance.StartRound += SetTimer;
    }

    private void SetTimer()
    {
        timer = MaxTimer;
    }

    public void Tick()
    {
        if(frozen)
            return;

        timer -= Time.deltaTime;
        GameUi.instance.SetTimer(timer);

        if(timer <= 0)
            GameManager.instance.RunEvent(false);
    }

    public IEnumerator Freeze(float freezeTime)
    {
        frozen = true;
        yield return new WaitForSeconds(freezeTime);
        frozen = false;
    }
}
