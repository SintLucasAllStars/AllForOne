using UnityEngine;

namespace MechanicFever
{
    public class Powerup : MonoBehaviour
    {
        [SerializeField]
        private PowerupType _powerupType;
        public PowerupType PowerupType => _powerupType;

        private void Awake() => _powerupType = (PowerupType)Random.Range(0, (int)System.Enum.GetNames(typeof(PowerupType)).Length);
    }

    public enum PowerupType
    {
        Adrenaline,
        Rage,
        TimeMachine
    }
}