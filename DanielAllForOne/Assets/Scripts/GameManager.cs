using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Field

    public static GameManager Instance;

    [SerializeField] private GameObject[] _powerUps;
    private int[,] _powerUpGrid = new int[20, 20];

    #endregion

    public void InitializePowerUpGrid()
    {
        for (int x = 0; x < _powerUpGrid.GetLength(0); x++)
        {
            for (int z = 0; z < _powerUpGrid.GetLength(1); z++)
            {
                int r = Random.Range(0, 20);

                if(r == 1)
                {
                    Vector3 pos = new Vector3(x + 0.5f - _powerUpGrid.GetLength(0), 0, z + 0.5f - _powerUpGrid.GetLength(1));

                    int r2 = Random.Range(0, _powerUps.Length);

                    Instantiate(_powerUps[r2], pos, Quaternion.identity);
                }
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
}
