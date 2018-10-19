using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Field

    public static GameManager Instance;

    [SerializeField] private GameObject[] _powerUpsObjects;
    [SerializeField] private GameManager[] _weaponsObjects;
    private int[,] _powerUpGrid = new int[20, 20];

    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void InitializePowerUpGrid()
    {
        for (int x = 0; x < _powerUpGrid.GetLength(0); x++)
        {
            for (int z = 0; z < _powerUpGrid.GetLength(1); z++)
            {
                int randomPowerUp = Random.Range(0, 30);
                int randomWeapon = Random.Range(0, 50);

                if (randomPowerUp == 1)
                {
                    Vector3 pos = new Vector3(x + 0.5f - _powerUpGrid.GetLength(0), 0, z + 0.5f - _powerUpGrid.GetLength(1));

                    int r2 = Random.Range(0, _powerUpsObjects.Length);

                    Instantiate(_powerUpsObjects[r2], pos, Quaternion.identity);
                }

                if (randomWeapon == 1)
                {
                    Vector3 pos = new Vector3(x + 0.5f - _powerUpGrid.GetLength(0), 0, z + 0.5f - _powerUpGrid.GetLength(1));

                    int r2 = Random.Range(0, _weaponsObjects.Length);

                    Instantiate(_weaponsObjects[r2], pos, Quaternion.identity);
                }
            }
        }
    }

}
