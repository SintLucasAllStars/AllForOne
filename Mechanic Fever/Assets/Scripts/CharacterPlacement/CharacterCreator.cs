   using UnityEngine;
using UnityEngine.UI;

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

    [Space()]
    [SerializeField] private Image background;

    [Space()]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider strengthSlider;
    [SerializeField] private Slider speedSlider;
    [SerializeField] private Slider defenceSlider;

    [Space()]
    [SerializeField] private Text points;
    [SerializeField] private Button createCharacterButton;
    [SerializeField] private Text hire;

    [Space()]
    [SerializeField] private GameObject UI;

    [Space()]
    [SerializeField] private Transform renderPosition;

    [Header("Spawning")]
    [SerializeField] private GameObject prefab;
    private Transform currentCharacter;
    private bool placingCharacter;
    private Camera cam;


    private void Start()
    {
        cam = GetComponent<Camera>();
        ResetUi();
    }

    private void Update()
    {
        if(!placingCharacter)
            return;

        RaycastHit hit;
        if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 200))
        {
            if(hit.collider.CompareTag("Floor"))
            {
                currentCharacter.position = hit.point;
                if(Input.GetMouseButtonDown(0))
                {
                    EndPlacement();
                }
            }
        }
    }

    public void StartPlacement()
    {
        UI.SetActive(false);
        placingCharacter = true;
    }

    private void EndPlacement()
    {
        placingCharacter = false;
        Create(currentCharacter.GetComponent<Character>());
    }


    private void Delete()
    {
        Destroy(UI);
        Destroy(this);
    }

    // Use this for initialization
    private void Create(Character character)
    {
        SetStats(character);
        if(GameManager.instance.CreateCharacter(CalculatePoints()))
            ResetUi();
        else
            Delete();

    }

    private void SetStats(Character character)
    {
        float health = Mathf.Lerp(healthRange.x, healthRange.y, healthSlider.value);
        float speed = Mathf.Lerp(speedRange.x, speedRange.y, speedSlider.value);

        character.SetStats(health, strengthSlider.value, speed, defenceSlider.value);
    }

    private int CalculatePoints()
    {
        float currentValue = minPoints + (healthSlider.value * healthCost) + (strengthSlider.value * strengthCost) + (speedSlider.value * speedCost) + (defenceSlider.value * defenceCost);
        return Mathf.RoundToInt(Mathf.Clamp(currentValue, minPoints, maxPoints));
    }

    private void ResetUi()
    {
        Player currentPlayer = GameManager.instance.GetCurrentPlayer();
        player.text = "Player " + currentPlayer.playerIndex;
        playerPoints.text = "Points: " + currentPlayer.points;

        background.color = currentPlayer.color;
        hire.color = currentPlayer.color;

        SetRandomSliders();

        currentCharacter = SpawnCharacter(currentPlayer.playerTag, currentPlayer.material).transform;
        UI.SetActive(true);

    }

    private void SetRandomSliders()
    {
        healthSlider.value = Random.Range(0f, 1f);
        strengthSlider.value = Random.Range(0f, 1f);
        speedSlider.value = Random.Range(0f, 1f);
        defenceSlider.value = Random.Range(0f, 1f);

        SetPoints();

    }

    public void SetPoints()
    {
        points.text = "Costs: " + CalculatePoints();
        createCharacterButton.interactable = GameManager.instance.CheckCharacterPoints(CalculatePoints());
    }

    private GameObject SpawnCharacter(string tag, Material material)
    {
        Debug.Log("Spawn");
        GameObject g = Instantiate(prefab, renderPosition.position, Quaternion.Euler(Vector3.zero));
        g.tag = tag;
        g.GetComponentInChildren<SkinnedMeshRenderer>().material = material;
        return g;
    }

}
