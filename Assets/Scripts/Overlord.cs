using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord : Unit
{
    protected void Start()
    {
        Renderer[] cubeRenderer = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < cubeRenderer.Length; i++)
        {
            for (int j = 0; j < cubeRenderer[i].materials.Length; j++)
            {
                cubeRenderer[i].materials[j].SetColor("_Color", Player.GetColor(_gameData.PlayerSide));
            }
        }
    }
}
