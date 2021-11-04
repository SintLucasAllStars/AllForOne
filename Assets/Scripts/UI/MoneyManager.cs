using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public float money;

    public TextMeshProUGUI moneytext;

    // Start is called before the first frame update
    void Start()
    {
        moneytext.SetText("Money: " + money.ToString("000"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void Pay()
    //{
    //    // check if you have enough money.
    //    if (StoreManager.instance.price <= money)
    //    {
    //        money -= StoreManager.instance.price;
    //        moneytext.SetText("Money: " + money.ToString("000"));
    //        print("You have enough money");
    //        UnitManager.instance.OnCreation();
    //        StoreManager.instance.SpawnUnit();
    //    }
    //    else if(StoreManager.instance.price > money)
    //    {
    //        print("You don't have enough money");
    //    }
    //}
}
