using UnityEngine;

namespace AllForOne
{
    public class UnitTemplateButton : Button
    {
        [SerializeField]
        private UnitData _unitData;

        public override void Press()
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
