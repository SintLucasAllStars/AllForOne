using UnityEngine;
using UnityEngine.UI;
using Zenject;

public delegate void OnUnitEditorToggled(bool newState);

public interface IUnitEditorManager
{
    void ToggleUnitEditor(bool newState);
    event OnUnitEditorToggled onEditorToggled;
    Unit GetConfiguredUnit();
}

public class UnitEditorManager : MonoBehaviour, IUnitEditorManager
{
    public event OnUnitEditorToggled onEditorToggled;
    public GameObject ConfiguredUnitContainer;
    [SerializeField] private Unit ConfiguredUnit;
    [Inject] private ITurnManager _turnManager;
    [Inject] private IUnitManager _unitManager;
    [SerializeField] private Slider[] _sliders = new Slider[4];
    [SerializeField] private Text _priceText;
    [SerializeField] private Camera _placementCam;
    [SerializeField] private Camera _creatorCam;

    private void Start()
    {
        _turnManager.onSwitchTurn += OnPlayerSwitchTurn;
        ToggleUnitEditor(true);
        OnPlayerSwitchTurn(_turnManager.GetCurrentPlayer());
    }

    private void OnPlayerSwitchTurn(Player player)
    {
        if (ConfiguredUnitContainer.transform.childCount == 1)
        {
            Destroy(ConfiguredUnitContainer.transform.GetChild(0).gameObject);
        }
        ConfiguredUnit = _unitManager.PlaceUnit(Vector3.zero, player, ConfiguredUnitContainer.transform);
        SetRandomStats();
    }

    public void ToggleUnitEditor(bool newState)
    {
        gameObject.SetActive(newState);
        onEditorToggled?.Invoke(newState);
        _placementCam.enabled = !newState;
        _creatorCam.enabled = newState;
    }

    private void SetRandomStats()
    {
        foreach (Slider slider in _sliders)
        {
            slider.value = Random.Range(2, 8);
        }
    }

    private void ReCalculateUnitPrice()
    {
        _priceText.text = "Total price: " + ConfiguredUnit.CalcUnitPrice();
    }

    public void OnSpeedChanged(float newValue)
    {
        ConfiguredUnit.Speed = (int) newValue;
        ReCalculateUnitPrice();
    }

    public void OnHealthChanged(float newValue)
    {
        ConfiguredUnit.Health = (int) newValue;
        ReCalculateUnitPrice();
    }

    public void OnDefenseChanged(float newValue)
    {
        ConfiguredUnit.Defense = (int) newValue;
        ReCalculateUnitPrice();
    }

    public void OnStrengthChanged(float newValue)
    {
        ConfiguredUnit.Strength = (int) newValue;
        ReCalculateUnitPrice();
    }

    public Unit GetConfiguredUnit()
    {
        return ConfiguredUnit;
    }
}