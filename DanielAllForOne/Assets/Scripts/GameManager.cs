using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    #region Field

    public static GameManager Instance;

    private Player[] _PlayerArr;

    private int _currentPlayingPlayer = 0;

    public GameObject UnitPrefab;
    #endregion

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        _PlayerArr = new[]{
            new Player(Color.red),
            new Player(Color.blue)
        };
    }

    public int GetCurrentPlayingPlayer {
        get {
            return _currentPlayingPlayer;
        }
    }

    public IEnumerator UnitPlacement(UnitStats unitStats)
    {
        GameObject unit = InstantiateUnit();

        while (!Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction, Color.green);

            if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
            {
                unit.transform.position = new Vector3(hit.point.x, 1, hit.point.z);
            }

            yield return null;
        }

        unit.GetComponent<Unit>().UnitStats = unitStats;

    }

    public GameObject InstantiateUnit()
    {
        return Instantiate(UnitPrefab);
    }
}
