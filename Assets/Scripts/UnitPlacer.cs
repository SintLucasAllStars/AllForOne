using UnityEngine;
using UnityEngine.UI;

public class UnitPlacer : MonoBehaviour
{
    public Text unitPrice;
    public Text playerPoints;
    public Text currentPlayer;
    public Slider healthSlider, strengthSlider, speedSlider, defenseSlider;
    public Unit unitPrefab;
    public Material red, blue;

    void Update()
    {
        if (GameManager.GetGameManager().GetGameState() != GameManager.GameState.PreGame)
        {
            Destroy(this);
        }
        
        HandleUnitPlacing();
        HandleUI();
    }

    private int CalculatePrice()
    {
        return (int) healthSlider.value + (int) strengthSlider.value + (int) speedSlider.value + (int) defenseSlider.value;
    }

    private void HandleUnitPlacing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPos = Input.mousePosition;
            Camera camera = Camera.main;
            Ray ray = camera.ScreenPointToRay(clickPos);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit, 100) || !hit.collider.gameObject.CompareTag("Ground"))
            {
                return;
            }

            int price = CalculatePrice();
            GameManager gameManager = GameManager.GetGameManager();
            Player currentPlayer = gameManager.GetCurrentPlayer();
            
            if (currentPlayer.Withdraw(price))
            {
                Unit unit = Instantiate(unitPrefab, hit.point, Quaternion.identity);
                unit.Initialize(
                    healthSlider.value/healthSlider.maxValue, 
                    strengthSlider.value/strengthSlider.maxValue, 
                    speedSlider.value/speedSlider.maxValue, 
                    defenseSlider.value/defenseSlider.maxValue
                    );

                if (currentPlayer.GetColor() == Player.Color.Red)
                {
                    unit.GetComponent<Renderer>().material = red;
                }
                else
                {
                    unit.GetComponent<Renderer>().material = blue;
                }
                
                gameManager.SwitchPlayers();
            }
        }
    }

    private void HandleUI()
    {
        unitPrice.text = CalculatePrice() + "";
        Player player = GameManager.GetGameManager().GetCurrentPlayer();
        currentPlayer.text = player.GetName();
        playerPoints.text = player.GetPoints() + "";
    }
    
}