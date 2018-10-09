using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewCam : MonoBehaviour {

    [SerializeField] private Transform objectPosition;

	// Use this for initialization
	void Start () {
        GameManager.instance.StartRound += Delete;
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(objectPosition.position, Vector3.up, 20 * Time.deltaTime);
	}

    void Delete()
    {
        GameManager.instance.StartRound -= Delete;
        Destroy(gameObject);
    }
}
