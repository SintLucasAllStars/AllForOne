using System;
using UnityEngine;
using SimpleJSON;

public class Test : MonoBehaviour
{
    private static string filePath = "./Assets/Scripts/Weapon/WeaponStats.json";
    // Use this for initialization
    void Start()
    {
        var file = System.IO.File.ReadAllText(filePath);
        var jsonFile = JSON.Parse(file);

        foreach (var item in jsonFile.Keys)
        {
            var type = Enum.Parse(typeof(Weapon.TypeOfWeapon), item);
            var damage = jsonFile[item]["damage"];
            var speed = jsonFile[item]["speed"];
            var range = jsonFile[item]["range"];
            if (range == "null")
            {
                Debug.Log("null");
            }
            Debug.Log(damage.GetType());
            Debug.Log(damage + ":" + speed + ": " + range);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
