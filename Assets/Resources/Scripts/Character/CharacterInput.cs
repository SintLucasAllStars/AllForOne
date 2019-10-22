using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterInput : MonoBehaviour
{
    CharacterStats characterStats;
    public GameObject playerCam;
    bool canAttack = true;
    private void Start()
    {
        characterStats = GetComponent<CharacterStats>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire"))
        {
            if (canAttack)
            {
                StartCoroutine(Attack(characterStats.weaponSpeed));            }
            
        }
    }
    IEnumerator Attack(float attackSpeed)
    {
        canAttack = false;
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, transform.TransformDirection(Vector3.forward), out hit, characterStats.weaponRange))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<CharacterStats>().TakeDamage(characterStats.weaponDamage);
                print(hit.collider.gameObject.name);
            }
        }
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
    }
}
