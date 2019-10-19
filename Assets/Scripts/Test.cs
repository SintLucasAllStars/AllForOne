using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AllForOne
{
    public class Test : MonoBehaviour
    {
        [SerializeField]
        private int _price;
        [SerializeField]
        private string _unitName;

        public void Purchase()
        {
            if(!Player.Instance.Wallet.HasEnoughMoney(_price))
            {
                Debug.Log("Not enough money");
                return;
            }
            UnitPlacementSystem.Instance.SetUnit(_unitName);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
                Purchase();
        }
    }
}
