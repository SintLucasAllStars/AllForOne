using UnityEngine;

namespace AllForOne
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField]
        private UnitData _unitData;

        public void SetPlayerUnit()
        {
            CharacterCreationManager.Instance.UpdateValues(_unitData.Health, _unitData.Strength, _unitData.Speed, _unitData.Defense);
            //if(!Player.Instance.Wallet.CanWithdraw(_price))
            //{
            //    Debug.Log("Not enough money");
            //    return;
            //}
            //UnitPlacementSystem.Instance.SetUnit(_unitName, _price);
        }
    }
}
