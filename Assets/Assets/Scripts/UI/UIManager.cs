using System.Collections;
using System.Collections.Generic;
using MORPH3D;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;
using Slider = UnityEngine.UI.Slider;

namespace UI
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        public GameObject StartButton;

        public GameObject CharacterSelect;

        public GameObject CharacterRenderBG;

        private Material _material;

        public Text HealthText;
        public Text StrengthText;
        public Text SpeedText;
        public Text DefenseText;
        public Text TotalText;
        public GameObject playerbody;

        public float totalvalue = 10;

        private float health = 1;

        private float Strength = 1;

        private float Speed = 1;

        private float Defense = 1;

        public float expensivestats;

        public float cheapstats;

        public GameObject BuyButton;

        public Text BuyText;

        public Text MoneyLeft;

        public Text Player;

        public Text Click;
        // Use this for initialization
        private void Awake()
        {
            if (Instance==null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }
        void Start()
        {
            //_material = CharacterRenderBG.GetComponent<Renderer>().material;
           // _material.color = Color.red;
            //SetColor(GameManager.instance.GetCurrentPlayer());
        }

        // Update is called once per frame
        void Update()
        {

            

        }

        public void StartGame()
        {
            StartButton.SetActive(false);
            CharacterSelect.SetActive(true);
            
            //playerbody.GetComponent<M3DCharacterManager>();
           
            //Randomize look of character

        }

        public void QuitGame()
        {
            Application.Quit();
        }
        public void CharacterSelection()
        {

        }
        public void Options()
        {

        }

        public void SetColor(int whichplayer)
        {
            if (whichplayer == 1)
            {
                _material.color = Color.red;
                
            }
            else if (whichplayer == 2)
            {
                _material.color = Color.blue;
            }

        }

        public void UpdateMoneyLeft(int player, int money)
        {
            if (player == 1)
            {
                Player.text = "Player 1 Red";
                MoneyLeft.text = money.ToString();
            }else if (player == 2)
            {
                Player.text = "Player 2 Blue";
                MoneyLeft.text = money.ToString();
            }
        }

        public void ChangeHealthText(Slider slider)
        {
            //expensive
            
            health = Mathf.RoundToInt(slider.value);
            HealthText.text = health.ToString();
            updateExpensiveStats();
        }
        public void ChangeSpeedText(Slider slider)
        {
            //expensive
            Speed = Mathf.RoundToInt(slider.value);
            SpeedText.text = Speed.ToString();
            updateExpensiveStats();


        }

        public void updateExpensiveStats()
        {
            //1.5 cost
            expensivestats = (health + Speed) * 1.5f;
            ChangeTotalText();
        }

        public void ChangeStrengthText(Slider slider)
        {
            
            Strength = Mathf.RoundToInt(slider.value);
            StrengthText.text = Strength.ToString();
            updateCheapStats();
        }

        
        public void ChangeDefenseText(Slider slider)
        {
            
            Defense = Mathf.RoundToInt(slider.value);
            DefenseText.text = Defense.ToString();
            updateCheapStats();
        }

        public void updateCheapStats()
        {
            //0.5 cost
            cheapstats = (Strength + Defense) * 0.5f;
            ChangeTotalText();
        }
        public void ChangeTotalText()
        {
            
            totalvalue = Mathf.RoundToInt((cheapstats + expensivestats) / 4) ;
            if (totalvalue > 10)
            {
                totalvalue = Mathf.RoundToInt((cheapstats + expensivestats) / 4);
            }
            else
            {
                totalvalue = 10;
            }
            TotalText.text = "Total Cost: " + totalvalue;
            GameManager.instance.checkIfEnoughMoney(Mathf.RoundToInt(totalvalue));
        }

        public void BuyUnit()
        {
           
            if (GameManager.instance.checkIfEnoughMoney(Mathf.RoundToInt(totalvalue)))
            {
                ChangeTotalText();
                GameManager.instance.ChangeMoney(Mathf.RoundToInt(totalvalue));
                
                GameManager.instance.AddPlayer(playerbody, health,Strength,Speed,Defense);
                CharacterSelect.SetActive(false);
                Click.enabled = true;
            }
        }

        public void SetBuyButtonInActive()
        {
            BuyButton.GetComponent<Button>().interactable = false;
            BuyText.text = "Not Enough Money";
        }


        public void SetBuyButtonActive()
        {
            BuyButton.GetComponent<Button>().interactable = true;
            BuyText.text = "Buy";
        }
        
    }
}

