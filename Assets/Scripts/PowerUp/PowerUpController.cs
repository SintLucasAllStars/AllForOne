using System.Collections.Generic;
using System;
using SimpleJSON;

public class PowerUpController : PowerUp
{
    private static string filePath = "./Assets/Scripts/PowerUp/PowerUpStats.json";

    public Dictionary<TypeOfPowerUp, PowerUp> powerUpDict = new Dictionary<TypeOfPowerUp, PowerUp>();

    public PowerUpController(TypeOfPowerUp typeOfPowerUp, int duration, int value) : base(typeOfPowerUp, duration, value) { }

    void LoadIn()
    {
        var file = System.IO.File.ReadAllText(filePath);
        var jsonFile = JSON.Parse(file);

        var gameController = GameController.instance;

        foreach (var item in jsonFile.Keys)
        {
            TypeOfPowerUp type = (TypeOfPowerUp)Enum.Parse(typeof(TypeOfPowerUp), item.ToString());
            var duration = jsonFile[item]["duration"];
            var value = jsonFile[item][1];

            PowerUp powerUp = new PowerUp(type, value, duration);
            powerUpDict.Add(type, powerUp);
        }
    }
}