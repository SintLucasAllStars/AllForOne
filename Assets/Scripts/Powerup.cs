using UnityEngine;

namespace MechanicFever
{
    public class Powerup : ItemContainer
    {
        private PowerupData _powerupData;
        public PowerupData PowerupData => _powerupData;

        public void SetPowerupData(PowerupData powerupData) => _powerupData = powerupData;

        public override void SetPosition(Node node)
        {
            _powerupData.SetPosition(node);

            transform.localPosition = Node.ToVector(_powerupData.Position);

            Map.Instance.OccupyNode(node, this);
        }
    }

    public class PowerupData : ItemData
    {
        [SerializeField]
        private PowerupType _powerupType;
        public PowerupType PowerupType => _powerupType;

        public PowerupData(Node position, bool isActive, string type, string guid, bool isConnected, PowerupType powerupType, PlayerSide side) : base(position, isActive, type, guid, isConnected, side) => _powerupType = powerupType;

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