using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private Transform weaponHolder;
    [SerializeField] private ParticleSystem particle;

    private Weapon current;

    private void Start()
    {
        StartCoroutine(Spawn(0));
    }

    private void Update()
    {
        if(current)
        {
            weaponHolder.Rotate(0, Time.deltaTime * 25, 0);
            weaponHolder.position = new Vector3(weaponHolder.position.x, Mathf.PingPong(Time.time * 0.1f, 0.5f), weaponHolder.position.z);
        }
    }

    private void PickUp()
    {
        particle.gameObject.SetActive(false);
        current.OnEquip -= PickUp;
        current = null;
        StartCoroutine(Spawn(10));
    }

    private IEnumerator Spawn(float delay)
    {
        yield return new WaitForSeconds(delay);

        particle.gameObject.SetActive(true);
        current = Instantiate(weapons[Random.Range(0, weapons.Length)], weaponHolder).GetComponent<Weapon>();
        current.transform.position += Vector3.up;
        current.OnEquip += PickUp;
    }

}
