using System.Collections.Generic;
using System;
using SimpleJSON;

class WeaponController : Weapon
{

    public WeaponController() : base(0, 0, 0) { }

    private static string filePath = "./Assets/Scripts/Weapon/WeaponStats.json";

    public Dictionary<TypeOfWeapon, Weapon> WeaponDict = new Dictionary<TypeOfWeapon, Weapon>();

    public void LoadIn()
    {
        var file = System.IO.File.ReadAllText(filePath);
        var jsonFile = JSON.Parse(file);

        var gameController = GameController.instance;

        foreach (var item in jsonFile.Keys)
        {
            TypeOfWeapon type = Enum.Parse(typeof(TypeOfWeapon), item);
            var damage = jsonFile[item]["damage"];
            var speed = jsonFile[item]["speed"];
            var range = jsonFile[item]["range"];
            if (range == "null")
            {
                var _range = GetDiagonalSizePlayarea(gameController.width, gameController.depth);
                Weapon weapon = new Weapon(damage, speed, _range);
                WeaponDict.Add(type, weapon);
            }

            // Debug.Log(damage + ":" + speed + ": " + range);
        }
    }
}