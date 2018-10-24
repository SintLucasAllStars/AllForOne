using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    private Rigidbody rb;

    [HideInInspector]
    public float speed;
    [HideInInspector]
    public ushort defaultStrength;
    [HideInInspector]
    public ushort weaponDamage = 1;
    [HideInInspector]
    public ushort weaponCooldown = 1;
    [HideInInspector]
    public ushort weaponDistance = 0;
    [HideInInspector]
    public float lastShotTime;



    [HideInInspector]
    public string pickUpName = "";
    private PickUpWeapon pickUpWeapon;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update ()
    {
        transform.Translate(0, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime / 10f);
        transform.Rotate(0, Input.GetAxis("Horizontal") * 130f * Time.deltaTime, 0);


        //Debug.DrawLine(transform.position, transform.position + transform.forward * 10);

        if (Input.GetMouseButtonDown(0) && lastShotTime + weaponCooldown < Time.time)
        {
            lastShotTime = Time.time;

            RaycastHit hit;
            if (Physics.Raycast (transform.position, transform.forward, out hit, 1.5f + weaponDistance))
            {

              //  Debug.Log("Tag to " + hit.collider.name);

                Character enemy = hit.collider.GetComponent<Character>();
                if (enemy != null)
                {
                    enemy.TakeDamage((ushort)((weaponDamage * 2f) + (defaultStrength / 5f)));
                }
            }


        }

        if (Input.GetKeyDown(KeyCode.E) && pickUpWeapon != null)
        {
            GameControl.instance.currentTurnCharacter.OnChangedWeapon (pickUpWeapon.ShowWeapon());
            Destroy(pickUpWeapon.gameObject);
            pickUpWeapon = null;
            pickUpName = "";
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        PickUpWeapon puw = other.gameObject.GetComponent<PickUpWeapon>();

        if (other.tag == "Weapon" && puw != null)
        {
            pickUpWeapon = puw;
            pickUpName = puw.ShowWeapon().primaryWeapon.name;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Weapon")
        {
            pickUpWeapon = null;
            pickUpName = "";
        }
    }

}
