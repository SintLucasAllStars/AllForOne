using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    [SerializeField] private new Camera camera;
    [SerializeField] private float cameraOffset = 25;

    private Unit hovered = null;

    private bool ready = false;
    
    #endregion

    #region Methods
    //Create and cache particle player for this player.
    private void Awake()
    {
        particle = Instantiate(particle);
        particle.GetComponent<Renderer>().material.SetColor("_TintColor", color);
        particle.gameObject.SetActive(false);

        GameManager.instance.AddPlayer(this);
    }

    private void OnEnable()
    {
        if(GameManager.instance.isSpawnMode)
        {
            if (ready || points == 0)
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
        if(GameManager.instance.isSpawnMode)
        {
            HoverPlace();
        }
        else
        {
            HoverSelect();
            
            //If we are hovering over an owned Unit.
            if (Input.GetMouseButtonDown(0))
            {
                if (hovered)
                {
                    hovered.Select();
                }
            }
        }

        CameraMotion();
    }

    private void HoverPlace()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);

            //Are we hitting the floor layer.
            if (hit.collider.gameObject.layer == 9)
            {
                UnitCreator.instance.PreviewUnit(hit.point);

                if (Input.GetMouseButtonDown(0))
                {
                    if (UnitCreator.instance.Cost <= points)
                    {
                        points -= UnitCreator.instance.Cost;
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
            UnitCreator.instance.Unit.SetActive(false);
        }

    }

    //Check whether the mouse is hovering over an Unit.
    private void HoverSelect()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.green);

            Unit unit = null;

            //Check wether we are hitting a Unit.
            if (unit = hit.collider.GetComponent<Unit>())
            {
                //If we hit a unit that belongs to this player.
                //Cache it and enable selection visual.
                if (unit.owner == index)
                {
                    hovered = unit;

                    particle.gameObject.SetActive(true);
                    particle.transform.position = hovered.transform.position;
                }
            }
            else
            {
                hovered = null;
                particle.gameObject.SetActive(false);
            }
        }
    }

    //Move the camera within clamped range.
    private void CameraMotion()
    {
        float motionX = Input.GetAxis("Horizontal") * Time.deltaTime * 10;
        float motionY = Input.GetAxis("Vertical") * Time.deltaTime * 10;

        //Clamp camera position within set distance from the player.
        if (motionX > 0 && camera.transform.position.x > transform.position.x + cameraOffset)
            motionX = 0;

        if (motionX < 0 && camera.transform.position.x < transform.position.x - cameraOffset)
            motionX = 0;

        if (motionY > 0 && camera.transform.position.z > transform.position.z + cameraOffset)
            motionY = 0;

        if (motionY < 0 && camera.transform.position.z < transform.position.x - cameraOffset)
            motionY = 0;

        camera.transform.Translate(motionX, 0, motionY, Space.World);
    }
    #endregion
}
