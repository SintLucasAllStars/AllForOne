using UnityEngine;
using Zenject;

public class PlaceUnitController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [Inject] private IUnitManager _unitManager;
    [Inject] private ITurnManager _turnManager;
    [Inject] private IUnitEditorManager _unitEditor;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || IsMouseOverUI.check()) return;
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Floor"))
            {
                PlaceUnit(hit.point, _unitEditor.GetConfiguredUnit());
            }
        }
    }

    private void PlaceUnit(Vector3 pos, Unit unit)
    {
        unit.transform.SetParent(_unitManager.GetUnitContainer());
        unit.transform.position = pos;
        unit.GetComponent<Rotator>().Disable();
        _turnManager.NextPlayer(false);
    }
}