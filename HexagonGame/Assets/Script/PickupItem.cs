using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public Item thisItem;

    public Item[] items;

    public void GetRandomItem()
    {
        items = new Item[5];
        items[0] = new Item(1, 10, 0, "Punch", new GameObject(), WeaponType.Hand);
        items[1] = new Item(2, 10, 0, "PowerPunch", new GameObject(), WeaponType.Hand);
        items[2] = new Item(3, 8, 0, "Knife", new GameObject(), WeaponType.Sword);
        items[3] = new Item(8, 4, 1, "WarHammer", new GameObject(), WeaponType.Sword);
        items[4] = new Item(5, 3, 3, "Gun", new GameObject(), WeaponType.Gun);
        thisItem = items[Random.Range(0, 5)];
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<Actor>()) { return; }
        other.gameObject.GetComponent<Actor>().SetItem(thisItem);
        Destroy(this.gameObject);
    }
}
