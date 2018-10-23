using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerType
    {
        Adernaline = 10, Rage = 5, Freeze = 3
    }
    public PowerType currentType;

    public float endTime;
    public float boost;

    public void Start()
    {
        GameManager.instance.EndRound += Destroy;
    }


    private void Update()
    {
        transform.Rotate(Vector3.up * 10 * Time.deltaTime);
    }

    public void Activate()
    {
        if(currentType == PowerType.Freeze)
        {
            StartCoroutine(GameManager.instance.timer.Freeze((int)currentType));
        }
        endTime = Time.time + (int)currentType;
        boost = GetBoost();
    }

    public bool CheckActivity()
    {
        return Time.time > endTime;
    }

    private float GetBoost()
    {
        switch(currentType)
        {
            case PowerType.Adernaline:
                return 1.5f;
            case PowerType.Rage:
                return 1.1f;
        }
        return 0;
    }

    private void Destroy()
    {
        GameManager.instance.EndRound -= Destroy;
        Destroy(gameObject);
    }
}
