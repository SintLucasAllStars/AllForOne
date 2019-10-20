using UnityEngine;

namespace AllForOne
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField]
        private UnitData _unitData;

        public void SetPlayerUnit()
        {
            Player.Instance.SetPlayerUnit(_unitData);
            //if(!Player.Instance.Wallet.CanWithdraw(_price))
            //{
            //    Debug.Log("Not enough money");
            //    return;
            //}
            //UnitPlacementSystem.Instance.SetUnit(_unitName, _price);
        }
    }
}
