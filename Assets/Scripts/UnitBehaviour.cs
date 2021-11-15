using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class UnitBehaviour : MonoBehaviour
{
    private Unit stats;
    private bool isInside = true;
    private bool isActive = false;
    private int unitOwner = 0;
    private GameManager gameManager;
    public vThirdPersonInput tpInput;
    public Rigidbody rb;
    public Animator anim;

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;
    private Vector3 origin;
    private Vector3 offset;
    private Vector3 direction;
    private UnitBehaviour hitUnit;
    private float hitDistance;

    private void Start()
    {
        offset = new Vector3(0f, 1.2f, 0f);
    }

    public void Update()
    {
        if (isActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckHit();
            }
        }
    }

    public void AddStats(Unit unit, int owner)
    {
        stats = unit;
        unitOwner = owner;
    }

    public Unit GetUnit()
    {
        return stats;
    }

    public int GetOwner()
    {
        return unitOwner;
    }

    public void PassGameManager(GameManager gm)
    {
        gameManager = gm;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("OutsideArea"))
        {
            isInside = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OutsideArea"))
        {
            isInside = true;
        }    
    }

    public void LockMovement()
    {
        tpInput.enabled = false;
        rb.isKinematic = true;
        rb.useGravity = false;
        anim.enabled = false;
        isActive = false;
    }

    public void UnlockMovement()
    {
        tpInput.enabled = true;
        rb.isKinematic = false;
        rb.useGravity = true;
        anim.enabled = true;
        isActive = true;
    }

    private void OnMouseDown()
    {
        /* Check if the unit selection screen is active and if the owner corresponds.
        Pass this unit as the active unit, then start the gameplay. */
        if (gameManager.IsUnitSelectionScreen() && gameManager.GetCurrentPlayer().GetPlayerNumber() == unitOwner)
        {
            gameManager.SetGameplayActive(this);
            UnlockMovement();    
        }
    }

    public void CheckHealth()
    {
        if (stats.GetHealth() <= 0)
        {
            gameManager.DestroyUnit(this.transform.gameObject);
        }
    }

    public bool CheckIfInside()
    {
        return isInside;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Debug.DrawLine(origin, origin + direction * hitDistance);
        Gizmos.DrawWireSphere(origin + direction * hitDistance, sphereRadius);
    }

    private void CheckHit()
    {
        origin = transform.position + offset;
        direction = transform.forward;
        RaycastHit hit;

        if (Physics.SphereCast(origin, sphereRadius, direction, out hit, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            hitDistance = hit.distance;
            if (unitOwner == 1)
            {
                if (hit.transform.gameObject.CompareTag("BlueUnit"))
                {
                    hitUnit = hit.transform.gameObject.GetComponent<UnitBehaviour>();
                    hitUnit.GetUnit().SubtractHealth(10);
                    hitUnit.CheckHealth();
                }
            }
            else if (unitOwner == 2)
            {
                if (hit.transform.gameObject.CompareTag("RedUnit"))
                {
                    hitUnit = hit.transform.gameObject.GetComponent<UnitBehaviour>();
                    hitUnit.GetUnit().SubtractHealth(10);
                    hitUnit.CheckHealth();
                }
            }
        }
        else
        {
            hitDistance = maxDistance;
            hitUnit = null;
        }
    }
}
