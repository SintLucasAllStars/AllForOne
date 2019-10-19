using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllForOne
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField]
        private int _price;
        [SerializeField]
        private string _unitName;

        public void Purchase()
        {
            if(!Player.Instance.Wallet.CanWithdraw(_price))
            {
                Debug.Log("Not enough money");
                return;
            }
            UnitPlacementSystem.Instance.SetUnit(_unitName, _price);
        }
    }
}
