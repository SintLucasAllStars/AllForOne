using System;
using System.Collections;
using System.Linq;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        private int _characterCountPlayerOne;        
        private int _characterCountPlayerTwo;

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
            _playersArray[character.OwnedByPlayer - 1].RemoveCharacter(character);
            
            if (_playersArray[character.OwnedByPlayer -1].Characters.Count == 0)
            {

                StartCoroutine(IEDelayCall());
            }
        }

        private IEnumerator IEDelayCall()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("HouseScene");
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
            Character character = new Character(strength,defence,speed,health, _currentlyActive,GetCharacterCount(ownedByPlayer));
            GetCurrentlyActivePlayer().AddCharacter(character);
            GameObject go =Instantiate(Prefabs.SmallTeddyBear, transform.position, Quaternion.identity);
            go.GetComponent<CharacterMono>().MyCharacter = character;
            character.MyCharacterMono = go.GetComponent<CharacterMono>();
            character.MyCharacterMono.SetPropertyColor(characterColor);
            SetCharacterCount(ownedByPlayer);
        }

        public Vector3 GetCurrentCharacterForwardPosition()
        {
            return GetCurrentlyActivePlayer().GetCurrentlyActiveCharacter().MyCharacterMono.transform.forward;
        }

        private int GetCharacterCount(int ownedByPlayer)
        {
            return ownedByPlayer == 1 ? _characterCountPlayerOne : _characterCountPlayerTwo;
        }

        private void SetCharacterCount(int ownedByPlayer)
        {
            if (ownedByPlayer == 1)
            {
                _characterCountPlayerOne++;
                
            }
            else
            {
                _characterCountPlayerTwo++;
            }
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
