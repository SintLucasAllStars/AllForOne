using UnityEngine.UI;
using UnityEngine;

public class CharacterCreator : MonoBehaviour
{

    private const int maxPoints = 100;
    private const int minPoints = 10;

    [Header("Stats")]
    [SerializeField] private Vector2 healthRange;
    [SerializeField] private float healthCost;

    [SerializeField] private Vector2 speedRange;
    [SerializeField] private float speedCost;

    [SerializeField] private float defenceCost;

    [SerializeField] private float strengthCost;


    [Header("UI")]
    [SerializeField] private Text player;
    [SerializeField] private Text playerPoints;
    [SerializeField] private Image background;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider strengthSlider;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider defenceSlider;
    [SerializeField] private Text points;
    [SerializeField] private Button createCharacterButton;
    [SerializeField] private Text hire;

    [SerializeField] private GameObject UI;


    [SerializeField] private Transform renderPosition;

    [Header("Spawning")]
    [SerializeField] private GameObject prefab;
    [SerializeField] private LayerMask layer;
    private GameObject currentCharacter;
    private bool placingCharacter;
    private Camera cam;


    private void Start()
    {
        GameManager.instance.StartRound += OnStartRound;
        cam = GetComponent<Camera>();
        ResetUI();
    }

    private void Update()
    {
        if(placingCharacter)
        {
            RaycastHit hit;
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 200, layer   ))
            {
                if(hit.collider.CompareTag("Floor"))
                {
                    currentCharacter.transform.position = hit.point;
                    if(Input.GetMouseButtonDown(0))
                    {
                        EndPlacement();
                    }
                }
            }
        }
    }

    public void StartPlacement()
    {
        UI.SetActive(false);
        placingCharacter = true;
    }

    void EndPlacement()
    {
        placingCharacter = false;
        GameManager.instance.CreateCharacter(CalculatePoints());
        ResetUI();
    }

    void OnStartRound()
    {
        GameManager.instance.StartRound -= OnStartRound;
        Destroy(UI);
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

        character.SetStats(health, strengthSlider.value, speed, defenceSlider.value);
    }

    public void OnSliderValueChanged()
    {
        points.text = "Costs: " + CalculatePoints();
        createCharacterButton.interactable = GameManager.instance.CheckCharacterPoints(CalculatePoints());
    }

    bool CheckPlayerPointsBool()
    {
        return GameManager.instance.CheckCharacterPoints(CalculatePoints());
    }

    int CalculatePoints()
    {
        float currentvalue = minPoints + (healthSlider.value * healthCost) + (strengthSlider.value * strengthCost) + (speedSlider.value * speedCost) + (defenceSlider.value  * defenceCost);
        return Mathf.RoundToInt(Mathf.Clamp(currentvalue, minPoints, maxPoints));
    }

    void ResetUI()
    {
        Player currentPlayer = GameManager.instance.GetCurrentPlayer();
        player.text = "Player " + currentPlayer.playerIndex;
        playerPoints.text = "Points: " + currentPlayer.points;

        background.color = currentPlayer.color;
        hire.color = currentPlayer.color;

        healthSlider.value = Random.Range(0f, 1f);
        strengthSlider.value = Random.Range(0f, 1f);
        speedSlider.value = Random.Range(0f, 1f);
        defenceSlider.value = Random.Range(0f, 1f);

        currentCharacter = Instantiate(prefab, renderPosition.position, Quaternion.Euler(Vector3.zero));
        UI.SetActive(true);

        OnSliderValueChanged();
    }
}
