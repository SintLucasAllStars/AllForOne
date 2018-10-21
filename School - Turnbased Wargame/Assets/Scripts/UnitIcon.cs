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

            foreach (GameObject p in PlayerManager.instance.playerBlue.playerGameObject)
            {
                Instantiate(unitIcon, p.transform);
            }

            Destroy(this);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
