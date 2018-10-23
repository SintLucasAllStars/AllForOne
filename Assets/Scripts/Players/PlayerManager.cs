using System;
using System.Linq;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Players
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public float StrengthCost = 2;
        public float HealthCost = 3;
        public float SpeedCost = 3;
        public float DefenceCost = 2;

        [SerializeField]
        public CharacterPrefabs Prefabs;


        [SerializeField] private Player[] _playersArray;
        public int AmountOfPlayers
        {
            get
            {
                return _playersArray.Length;
            }

        }


        public  float GetTotalCost()
        {
            return StrengthCost + HealthCost + DefenceCost + SpeedCost;
        }

        private int _currentlyActive = 1;

        public Player GetCurrentlyActivePlayer()
        {
            return _playersArray[_currentlyActive - 1];
        }

        public void RemoveCharacter(Character character)
        {
            GetCurrentlyActivePlayer().RemoveCharacter(character);
        }

        public bool SetCurrentlyActivePlayerSelection()
        {
            while (true)
            {
                GameManager.Instance.ChoosingCharacters = StillChoosing();
                if (GameManager.Instance.ChoosingCharacters)
                {
                    if (_currentlyActive + 1 > AmountOfPlayers)
                    {
                        _currentlyActive = 1;
                    }
                    else
                    {
                        _currentlyActive++;
                    }

                    if (!GetCurrentlyActivePlayer().Choosing)
                    {
                        continue;
                    }

                    return true;
                }
                return false;


            }

            Debug.Log(StillChoosing());

        }

        public void SetCurrentlyActivePlayer()
        {
            if (_currentlyActive + 1 > AmountOfPlayers)
            {
                _currentlyActive = 1;
            }
            else
            {
                _currentlyActive++;
            }
        }


        public void SetRandomPlayer()
        {
            _currentlyActive = Random.Range(1, _playersArray.Length + 1);
            Debug.Log("Player " + _currentlyActive + " Chosen");
        }
        
        
   

        private bool StillChoosing()
        {
            return GameManager.Instance.ChoosingCharacters && _playersArray.Any(t => t.Choosing);
        }


        public void AddCharacterToCurrentPlayerAndInstansiate(float strength, float defence, float speed, float health, int ownedByPlayer, Color characterColor)
        {
            Character character = new Character(strength,defence,speed,health, _currentlyActive);
            GetCurrentlyActivePlayer().AddCharacter(character);
            GameObject go =Instantiate(Prefabs.SmallTeddyBear, transform.position, Quaternion.identity);
            go.GetComponent<CharacterMono>().MyCharacter = character;
            character.MyCharacterMono = go.GetComponent<CharacterMono>();
            character.MyCharacterMono.SetPropertyColor(characterColor);
        }

        public Vector3 GetCurrentCharacterForwardPosition()
        {
            return GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().MyCharacterMono.transform.forward;
        }


        public Character GetCurrentlyActiveCharacter()
        {
            return GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter();
        }


        private void Start()
        {
            _playersArray = new Player[2];


            InitializePlayer();
        }

        private void InitializePlayer()
        {

            for (int i = 0; i < _playersArray.Length; i++)
            {
               _playersArray[i] = new Player(i + 1);
            }
        }




    }
}

[Serializable]
public class CharacterPrefabs
{
    public GameObject SmallTeddyBear;
}
