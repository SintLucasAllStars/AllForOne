using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlacement : MonoBehaviour {

    [SerializeField] GameObject prefab;
    GameObject currentSpawnedCharacter;
    bool moveCharacter;


	// Use this for initialization
	void Create () {
        moveCharacter = true;
        currentSpawnedCharacter = Instantiate(prefab, Vector3.zero, Quaternion.identity);
	}
	


	// Update is called once per frame
	void Update () {
		//Raycast ``````````````````
	}
}
