using System.Collections.Generic;
using UnityEngine;

public interface IUnitManager
{
    Unit PlaceUnit(Vector3 pos, Player owner, Transform parent);
    Transform GetUnitContainer();
}

public class UnitManager : MonoBehaviour, IUnitManager
{
    public List<GameObject> unitPrefabsRed;
    public List<GameObject> unitPrefabsBlue;

    private List<Unit> _units = new List<Unit>();

    public Unit PlaceUnit(Vector3 pos, Player owner, Transform parent = null)
    {
        if (parent == null) parent = transform;

        Unit unit = GetRandomUnit(owner, parent);
        unit.OwnedBy = owner;
        unit.transform.localPosition = pos;
        _units.Add(unit);
        return unit;
    }

    private Unit GetRandomUnit(Player player, Transform parent)
    {
        GameObject unit = null;
        if (player.Color == Color.blue)
        {
            unit = Instantiate(unitPrefabsBlue[Random.Range(0, unitPrefabsBlue.Count - 1)], parent);
        }
        else
        {
            unit = Instantiate(unitPrefabsRed[Random.Range(0, unitPrefabsRed.Count - 1)], parent);
        }

        return unit.GetComponent<Unit>();
    }

    public Transform GetUnitContainer()
    {
        return transform;
    }
}