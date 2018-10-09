using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    [SerializeField] LayerMask placeLayer, InsideSpawnPlaceLayer;
    public GameObject prefab;
    private Camera cam;


    public SoldierAsset soldier01;

    private void Start()
    {
        cam = Camera.main;
    }


    private void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 50f, placeLayer))
        {
            prefab.transform.position = hit.point;

            if (hit.collider.gameObject.layer == Mathf.Log(InsideSpawnPlaceLayer.value, 2))
            {
                if (Input.GetMouseButtonUp(0))
                {
                    GameObject spawnUnit = Instantiate(soldier01.unitSoldier.objectMesh, hit.point, Quaternion.identity) as GameObject;
                    spawnUnit.AddComponent<Character>().init(soldier01.unitSoldier);
                    PlayerManager.instance.playerRed.playerGameObject.Add(spawnUnit);

                    //PlayerManager.instance.playerRed[0].playerNormalStats.name = "hello";
                    Debug.Log("First char: " + PlayerManager.instance.playerRed[0].playerNormalStats.name);
                    Debug.Log("Last char: " + spawnUnit.GetComponent<Character>().playerNormalStats.health);


                }
            }
        }



    }

}
