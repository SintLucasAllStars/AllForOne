using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    //Will handle the spawning of the units.
    //Spawns with the correct values that has been set by the sliders.
    //Adds an tag for the current team that is spawning the units.
    //Adds the unit to an array.
    public void Spawn(Transform transformToSpawn)
    {
        GameObject unitToSpawn = UnitConfig.Instance.unitToSpawn;
        Slider[] sliders = UnitConfig.Instance.sliders;
        string teamTag = UnitConfig.Instance.teamTag;

        GameObject unit = Instantiate(unitToSpawn, transformToSpawn.position, Quaternion.identity);
        unit.GetComponent<Unit>().SpawnWithValues(((int)sliders[0].value), ((int)sliders[1].value), ((int)sliders[2].value), ((int)sliders[3].value));
        unit.tag = teamTag;
        //spawnedUnits.Add(unit);
    }
}
