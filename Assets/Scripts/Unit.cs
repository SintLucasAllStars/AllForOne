using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour
{
    [System.Serializable]
    public struct Stats
    {
        public float health;
        public float strength;
        public float speed;
        public float defence;
    }

    public float Timer { get { return timer; } }

    public int owner = 0;
    public float speedRotation = 2;
    public float speedMovement = 2;

    public Stats stats;

    private Animator animator;

    private bool isSelected = false;

    private float timer = 10;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if(isSelected)
        {
            if(timer >= 0)
            {
                timer -= Time.unscaledDeltaTime;

                float x = Input.GetAxis("Horizontal") * Time.deltaTime * 10 * speedRotation;
                float y = Input.GetAxis("Vertical");

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    y *= 2;
                }

                animator.SetFloat("Speed", y);
                transform.Rotate(0, x, 0);
            }
            else
            {
                DeSelect();
                animator.SetFloat("Speed", 0);
            }
        }
    }

    public void Select()
    {
        UnitUI.instance.DisplayUI(this, true);
        timer = 10;
        isSelected = true;
        UnitCamera.instance.SetUnitCamera(this);
    }

    public void DeSelect()
    {
        UnitUI.instance.DisplayUI(this, false);
        isSelected = false;
        UnitCamera.instance.SetPlayerCamera();
        GameManager.instance.NextTurn();
    }
}
