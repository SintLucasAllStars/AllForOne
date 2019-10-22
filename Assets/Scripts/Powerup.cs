using UnityEngine;

namespace MechanicFever
{
    public class PowerupContainer : MonoBehaviour
    {
        private PowerupData _powerupData;
        public PowerupData PowerupData => _powerupData;

        public void SetPowerupData(PowerupData powerupData) => _powerupData = powerupData;
    }

    public class PowerupData : ItemData
    {
        [SerializeField]
        private PowerupType _powerupType;
        public PowerupType PowerupType => _powerupType;

        public PowerupData(string type, string guid, bool isConnected, PowerupType powerupType) : base(type, guid, isConnected) => _powerupType = powerupType;

        public PowerupData(PowerupData powerupData) : base(powerupData) => _powerupType = powerupData._powerupType;

        public PowerupData()
        { }
    }

    public enum PowerupType
    {
        Adrenaline,
        Rage,
        TimeMachine
    }
}