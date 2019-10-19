using UnityEngine;

namespace AllForOne
{
    public class Overlord : Unit
    {
        new protected void Start()
        {
            base.Start();

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
}