using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int UnitCount { get { return unitCount; } set { unitCount = value; if(unitCount <= 0) { GameManager.instance.PlayerRemove(this); } } }
    public bool Ready { get { return ready; } set { ready = value; GameManager.instance.NextTurn(); } }

    #region Fields
    [Header("Stats")]
    public int index        = 0;
    public new string name  = "Player";
    public Color color      = Color.white;
    public int points       = 100;
    public bool turn        = false;
    [Space]
    [SerializeField] private ParticleSystem particle = null;
    [SerializeField] private LayerMask rayMask = 0;

    private new Camera camera;

    private bool ready = false;

    private int unitCount = 0;
    #endregion

    #region Methods
    //Create and cache particle player for this player.
    private void Awake()
    {
        particle = Instantiate(particle);
        particle.GetComponent<Renderer>().material.SetColor("_TintColor", color);
        particle.gameObject.SetActive(false);

        if (name == "Player")
            name = "Player_" + index;

        GameManager.instance.PlayerAdd(this);
        camera = GlobalCamera.instance.Camera;
    }

    private void OnEnable()
    {
        if(GameManager.instance.SpawnMode)
        {
            if (ready)
            {
                GameManager.instance.NextTurn();
                return;
            }
                
            UnitCreator.instance.Player = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.SpawnMode)
        {
            HoverPlace();
        }
        else
        {
            HoverSelect();
        }
    }

    private void HoverPlace()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30, rayMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);

            //Are we hitting the floor layer.
            if (hit.collider.gameObject.layer == 9)
            {
                UnitCreator.instance.PreviewUnit(hit.point);

                //Input for placing the unit.
                if (Input.GetMouseButtonDown(0))
                {
                    if (UnitCreator.instance.Cost <= points)
                    {
                        if ((points -= UnitCreator.instance.Cost) == 0)
                            ready = true;

                        UnitCreator.instance.SpawnUnit(hit.point);
                        GameManager.instance.NextTurn();
                    }
                }
            }
            else
            {
                UnitCreator.instance.Unit.SetActive(false);
            }
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.direction * 10, Color.white);
            UnitCreator.instance.Unit.SetActive(false);
        }

    }

    //Check whether the mouse is hovering over an Unit.
    private void HoverSelect()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 30, rayMask))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);

            Unit unit = null;

            //Check wether we are hitting a Unit.
            if (unit = hit.collider.GetComponent<Unit>())
            {
                //If we hit a unit that belongs to this player.
                //Cache it and enable selection visual.
                if (unit.owner == this)
                {
                    particle.gameObject.SetActive(true);
                    particle.transform.position = unit.transform.position;

                    //If we are hovering over an owned Unit.
                    if (Input.GetMouseButtonDown(0))
                    {
                        unit.Select();
                        enabled = false;
                        particle.gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                particle.gameObject.SetActive(false);
            }
        }
    }
    #endregion
}
