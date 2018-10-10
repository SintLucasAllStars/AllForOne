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

    public int owner = 0;
    public float speedRotation = 2;
    public float speedMovement = 2;

    public Stats stats;

    private Animator animator;

    private bool isSelected = false;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if(isSelected)
        {
            float x = Input.GetAxis("Horizontal") * Time.deltaTime * 10 * speedRotation;
            float y = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift))
            {
                y *= 2;
            }

            animator.SetFloat("Speed", y);
            transform.Rotate(0, x, 0);
        }
    }

    public void Select()
    {
        isSelected = true;
        UnitCamera.instance.SetUnitCamera(this);
    }
}
