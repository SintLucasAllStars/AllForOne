using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
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
    
    #endregion

    #region Methods
    //Create and cache particle player for this player.
    private void Awake()
    {
        particle = Instantiate(particle);
        particle.GetComponent<Renderer>().material.SetColor("_TintColor", color);
        particle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Hover();
        CameraMotion();

        //If we are hovering over an owned Unit.
        if(Input.GetMouseButtonDown(0))
        {
            if(hovered)
            {
                hovered.Select();
            }
        }
    }

    //Check whether the mouse is hovering over an Unit.
    private void Hover()
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
