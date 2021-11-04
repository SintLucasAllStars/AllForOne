using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<GameObject> units = new List<GameObject>();

    public float money;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AbleToBuy(float price)
    {
        if (price <= money)
        {
            StoreManager.instance.Pay();
        }
        else if (price > money)
        {
            
        }
    }
}
