using System.Collections.Generic;
using System;
using SimpleJSON;

public class PowerUpController : PowerUp
{
    private static string filePath = "./Assets/Scripts/PowerUp/PowerUpStats.json";

    public Dictionary<TypeOfPowerUp, PowerUp> powerUpDict = new Dictionary<TypeOfPowerUp, PowerUp>();

    public PowerUpController(string name, int duration) : base(name, duration)
    {

    }

    void LoadIn()
    {
        var file = System.IO.File.ReadAllText(filePath);
        var jsonFile = JSON.Parse(file);

        var gameController = GameController.instance;

        foreach (var item in jsonFile.Keys)
        {
            TypeOfPowerUp type = (TypeOfPowerUp)Enum.Parse(typeof(TypeOfPowerUp), item.ToString());
            var duration = jsonFile[item]["duration"];
            var speed = jsonFile[item][type.ToString()];
            PowerUp powerUp = new PowerUp(type.ToString(), duration);
            powerUpDict.Add(type, powerUp);
        }
    }
}