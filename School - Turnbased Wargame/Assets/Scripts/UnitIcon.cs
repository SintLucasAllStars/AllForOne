using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIcon : MonoBehaviour
{
    public GameObject unitIcon;


	void OnEnable ()
    {
        if (gameObject.activeInHierarchy)
        {
            foreach (GameObject p in PlayerManager.instance.playerRed.playerGameObject)
            {
                Instantiate(unitIcon, p.transform);
            }
            //MapManager.instance.MapDeck(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
