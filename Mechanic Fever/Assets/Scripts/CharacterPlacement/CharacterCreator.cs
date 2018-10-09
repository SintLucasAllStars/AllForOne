using UnityEngine.UI;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{

    private const int maxPoints = 100;
    private const int minPoints = 10;

    [Header("Stats")]
    [SerializeField] private Vector2 healthRange;
    [SerializeField] private Vector2 speedRange;
    [SerializeField] private Vector2 defenceRange;

    [Header("UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider damageSlider;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider defenceSlider;
    [SerializeField] private Button createCharacterButton;

    private void Start()
    {
        GameManager.instance.StartRound += OnStartRound;
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.C))
            if(CheckPlayerPointsBool())
                StartPlacement();
    }

    void StartPlacement()
    {

    }

    void OnStartRound()
    {
        GameManager.instance.StartRound -= OnStartRound;
        Destroy(this);
    }

    // Use this for initialization
    void Create(Character character)
    {
        SetStats(character);

        GameManager.instance.CreateCharacter(CalculatePoints());

        ResetUI();
    }

    void SetStats(Character character)
    {
        float health = Mathf.Lerp(healthRange.x, healthRange.y, healthSlider.value);
        float speed = Mathf.Lerp(speedRange.x, speedRange.y, speedSlider.value);
        float defence = Mathf.Lerp(defenceRange.x, defenceRange.y, defenceSlider.value);

        character.SetStats(health, defenceSlider.value, speed, defence);
    }

    void CheckPlayerPoints()
    {
        createCharacterButton.interactable = GameManager.instance.CheckCharacterPoints(CalculatePoints());
    }

    bool CheckPlayerPointsBool()
    {
        return GameManager.instance.CheckCharacterPoints(CalculatePoints());
    }

    int CalculatePoints()
    {
        float currentvalue = healthSlider.value + damageSlider.value + speedSlider.value + defenceSlider.value;
        currentvalue /= 4;
        return Mathf.RoundToInt(Mathf.Lerp(minPoints, maxPoints, currentvalue));
    }

    void ResetUI()
    {
        healthSlider.value = Random.Range(0,1);
        damageSlider.value = Random.Range(0, 1);
        speedSlider.value = Random.Range(0, 1);
        defenceSlider.value = Random.Range(0, 1);

        CheckPlayerPoints();
    }
}
