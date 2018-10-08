using Tools;
using UnityEngine;

namespace Players
{
    public class PlayerManager : Singleton<PlayerManager>
    {
        public int StrengthCost = 2;
        public int HealthCost = 3;
        public int SpeedCost = 3;
        public int DefenceCost = 2;
        
        
        
        private Player[] _playersArray;
        public int AmountOfPlayers
        {
            get
            {
                return _playersArray.Length;
            }
        }


        private int _currentlyActive = 1;
       
        public Player GetCurrentlyActivePlayer()
        {
            return _playersArray[_currentlyActive];
        }
        
      
        

        private void Start()
        {
            InitializePlayer();
        }

        private void InitializePlayer()
        {   
            _playersArray = new Player[_playersArray.Length];        
            for (int i = 0; i < _playersArray.Length; i++)
            {
               _playersArray[i] = new Player(i + 1);
            }
        }


    }
}
