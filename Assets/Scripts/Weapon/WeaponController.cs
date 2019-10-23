using System.Collections.Generic;
using System;
using SimpleJSON;

class WeaponController
{
    private static string filePath = "./Assets/Scripts/Weapon/WeaponStats.json";

    public static Dictionary<Weapon.TypeOfWeapon, Weapon> WeaponDict = new Dictionary<Weapon.TypeOfWeapon, Weapon>();

    public WeaponController()
    {
        LoadIn();
    }

    public void LoadIn()
    {
        var file = System.IO.File.ReadAllText(filePath);
        var jsonFile = JSON.Parse(file);

        var gameController = GameController.instance;

        foreach (var item in jsonFile.Keys)
        {
            Weapon.TypeOfWeapon type = (Weapon.TypeOfWeapon)Enum.Parse(typeof(Weapon.TypeOfWeapon), item.ToString());
            var damage = jsonFile[item]["damage"];
            var speed = jsonFile[item]["speed"];
            var range = jsonFile[item]["range"];

            //if the weapon is a gun it will excute GetDiagonalSizePlayarea()
            //instead of getting the data out of the JSON file
            if (range == "null")
            {
                var _range = Weapon.GetDiagonalSizePlayarea(gameController.width, gameController.depth);
                Weapon weapon = new Weapon(damage, speed, _range, type);
                WeaponDict.Add(type, weapon);
                return;
            }
            else
            {
                Weapon weapon = new Weapon(damage, speed, range, type);
                WeaponDict.Add(type, weapon);
            }
        }
    }
}