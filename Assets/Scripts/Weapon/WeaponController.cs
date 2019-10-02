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
            TypeOfWeapon type = (TypeOfWeapon)Enum.Parse(typeof(TypeOfWeapon), item.ToString());
            var damage = jsonFile[item]["damage"];
            var speed = jsonFile[item]["speed"];
            var range = jsonFile[item]["range"];

            //if the weapon is a gun it will excute GetDiagonalSizePlayarea()
            //instead of getting the data out of the JSON file
            if (range == "null")
            {
                var _range = GetDiagonalSizePlayarea(gameController.width, gameController.depth);
                Weapon weapon = new Weapon(damage, speed, _range);
                WeaponDict.Add(type, weapon);
                return;
            }
            else
            {
                Weapon weapon = new Weapon(damage, speed, range);
                WeaponDict.Add(type, weapon);
            }
        }
    }
}