using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected enum UnitState { Idle, Attacking, Dead }

    //Reference Variables
    [SerializeField] protected UnitInterface unitInterface;
    [SerializeField] protected UnitCameraControl unitCameraControl;
    [SerializeField] protected UnitAnimation unitAnimation;
    protected BaseTeam team;

    //Unit Transform Variables
    protected Vector3 cameraOrigionalPosition;
    public Transform cameraTransform;
    public Transform weaponTransform;

    //Standard Variables
    protected readonly int maxHitPoints = 100;
    protected int hitPoints;
    protected int speed = 10;
    protected int strength = 10;
    protected int defense = 10;
    protected int teamNumber;
    protected Weapon weapon;

    //Combat Variables
    public bool inCombat;
    public bool isAttacking;
    protected bool inCombatDelay;
    protected float inComatDelayTimer;

    
    [SerializeField] protected Unit target;

    //Movement Variables
    protected Rigidbody rb;
    protected Vector3 direction;

    //Active Turn Variables
    public bool isSelected = false;

    //Special Features Variables
    protected bool hasDriveby;
    protected bool drivebyUsed = false;

    protected bool hasOpportunist;
    protected bool opportunistUsed = false;

    protected bool hasTowershield = false;


    protected virtual void Awake()
    {
        //References
        cameraOrigionalPosition = cameraTransform.localPosition;
        rb = this.GetComponent<Rigidbody>();

        //Attributes
        speed = speed * 100;
        hitPoints = maxHitPoints;
    }

    protected virtual void FixedUpdate()
    {
        if ((isSelected) && (!inCombat))
        {
            if (rb.velocity != Vector3.zero)
            {
                unitAnimation.AnimMove(true);
            }
            else
            {
                unitAnimation.AnimMove(false);
            }

            Movement();

            //Activate combat when enemy is near the range of the Unit's weapon.
            if ((SeeEnemy()) && (!inCombatDelay) && (!drivebyUsed))
            {
                StartCombat();
            }
        }

        //Delay when an combat enounter had taken place and was ignored or attacked.
        if (inCombatDelay)
        {
            inComatDelayTimer -= Time.deltaTime;
            if (inComatDelayTimer < 0)
            {
                inCombatDelay = false;
            }
        }
    }

    /// <summary>
    /// Places a unit on the map.
    /// </summary>
    public void CreateUnit(int speed, int strength, int defense, int teamNumber, List<Feature> features, Weapon weapon, BaseTeam team)
    {
        this.speed = speed;
        this.strength = strength;
        this.defense = defense;

        this.teamNumber = teamNumber;
        this.weapon = weapon;

        this.team = team;

        CheckSpecialFeatures(features);
        unitInterface.SetSlider(hitPoints);

        cameraOrigionalPosition = cameraTransform.localPosition;
        this.speed = speed * 10;
    }

    /// <summary>
    /// On Click ignore combat en delay combat for 4 seconds.
    /// </summary>
    public void ButtonIgnoreCombat()
    {
        EndCombat(4);
    }

    /// <summary>
    /// Call this when you select a unit from the Overlook gamestate.
    /// </summary>
    public bool IsCorrectTeam(int teamNumber)
    {
        if (this.teamNumber == teamNumber)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Call this when damage is taken returns if the unit dies or not.
    /// </summary>
    public bool LethalDamage(int damage)
    {
        if (hitPoints - damage <= 0)
        {
            Death();
            return true;
        }

        RecieveDamage(damage);
        return false;
    }

    /// <summary>
    /// Called from GameManager, when this unit starts his turn.
    /// </summary>
    public void StartSelectedTurn()
    {
        isSelected = true;
        cameraTransform.localPosition = cameraOrigionalPosition;

        GameManager.gameManager.StartTurn(this);

    }

    /// <summary>
    /// Called from GameManager, when this unit ends his turn.
    /// </summary>
    public void StopSelectedTurn()
    {
        isSelected = false;
        if ((hasDriveby) && (drivebyUsed))
        {
            drivebyUsed = false;
            speed = speed / 2;
        }


        team.EndTurn();
    }

    /// <summary>
    /// Called from GameManager, when this unit ends his turn early.
    /// </summary>
    public void StopSelectedEarly()
    {
        isSelected = false;
        if ((hasDriveby) && (drivebyUsed))
        {
            drivebyUsed = false;
            speed = speed / 2;
        }
        //Set the Animation
        unitAnimation.AnimMove(false);
    }

    /// <summary>
    /// Called after the delayTimer from GameManager has ended.
    /// </summary>
    public virtual void EndAttack()
    {
        if (hasDriveby)
        {
            speed = speed * 2;
            drivebyUsed = true;
        }
        //Set the Animation
        unitAnimation.AnimMove(false);

        //Stop Movement
        rb.velocity = Vector3.zero;
    }

    /// <summary>
    /// Call this function when the Unit recieve damage.
    /// </summary>
    protected void RecieveDamage(int damage)
    {
        hitPoints = hitPoints - damage;

        unitInterface.UpdateSlider(hitPoints);
    }

    /// <summary>
    /// Makes you unable to engage in combat for a certain amount of time.
    /// </summary>
    protected void CombatDelay(float waitTime)
    {
        inCombatDelay = true;
        inComatDelayTimer = waitTime;
    }

    /// <summary>
    /// Call this when the HitPoints become 0 or lower.
    /// </summary>
    protected void Death()
    {
        team.RemoveUnitFromList(this);
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Called when an enemy is within the Weapon's range.
    /// </summary>
    protected virtual void StartCombat()
    {
        //Active Combat UI
        unitInterface.ActivateCombatCanvas(true);

        //Adjust Camera
        unitCameraControl.LookAtEnemy(target.transform);

        //Stop Movement
        inCombat = true;
        rb.velocity = Vector3.zero;

        //Set the Animation
        unitAnimation.AnimMove(false);

        //Set the timer on Gamemanager
        GameManager.gameManager.CombatTimer(true);

        Debug.Log("CombatStarted");
    }

    /// <summary>
    /// Ends and delays combat for a moment.
    /// </summary>
    protected void EndCombat(float waitTime)
    {
        //Deactivate Combat UI
        unitInterface.ActivateCombatCanvas(false);

        //Continue Movement
        inCombat = false;

        //Set the timer on the GameManager
        GameManager.gameManager.CombatTimer(false);

        //Set the Animation
        unitAnimation.AnimAttack(false);

        //Stop Movement
        rb.velocity = Vector3.zero;

        //Delay combat for a moment
        CombatDelay(waitTime);


        Debug.Log("CombatEnded");
    }

    /// <summary>
    /// Detects enemies through Overlapsphere. Every team has a layermask.
    /// </summary>
    protected bool SeeEnemy()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, weapon.range, 1 << 9 + teamNumber);
        if (hitColliders.Length == 0)
        {
            return false;
        }

        int i = 0;
        while (i < hitColliders.Length)
        {
            Vector3 direction = transform.position - hitColliders[i].transform.position;

            if (!IsObscured(direction, 1 << 9 + teamNumber))
            {
                target = hitColliders[i].gameObject.GetComponent<Unit>();
                i = hitColliders.Length;

                return true;
            }
            i++;
        }
        return false;
    }

    /// <summary>
    /// Check if the enemy is obscured by a obstacle like a wall.
    /// </summary>
    protected bool IsObscured(Vector3 direction, int layerMask)
    {
        if (Physics.Raycast(transform.position, -direction, out RaycastHit hit, weapon.range))
        {
            if (hit.collider.gameObject.CompareTag("AI"))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Lets the Unit move using the unit's speed. Is only active when it's the unit it's turn.
    /// </summary>
    protected void Movement()
    {
        rb.velocity = direction * speed * Time.deltaTime;

        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        direction = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
    }

    /// <summary>
    /// Activates the Special features that were bought in the UnitStore.
    /// </summary>
    protected void CheckSpecialFeatures(List<Feature> feats)
    {
        for (int i = 0; i < feats.Count - 1; i++)
        {
            if (feats[i].feat == UnitStore.BonusFeatures.Driveby)
            {
                hasDriveby = true;
            }
            if (feats[i].feat == UnitStore.BonusFeatures.Opportunist)
            {
                hasOpportunist = true;
            }
            if (feats[i].feat == UnitStore.BonusFeatures.TowerShield)
            {
                hasTowershield = true;
            }
        }
    }
}