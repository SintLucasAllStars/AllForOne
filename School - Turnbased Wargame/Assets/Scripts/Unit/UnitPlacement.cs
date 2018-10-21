using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    [SerializeField] LayerMask placeLayer, InsideSpawnPlaceLayer;
    public GameObject selectObject;
    private Camera cam;


    public SoldierAsset selectSoldier;

    private void Start()
    {
        cam = Camera.main;
    }


    private void Update()
    {
        if (selectSoldier != null)
        {
            if (selectObject == null)
            {
                selectObject = Instantiate(selectSoldier.unitSoldier.objectMesh) as GameObject;
            }

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50f, placeLayer))
            {
                selectObject.transform.position = hit.point;

                if (hit.collider.gameObject.layer == Mathf.Log(InsideSpawnPlaceLayer.value, 2))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        SpawnUnit(hit.point);
                    }
                } else
                {

                }
            }
        }
    }

    private void SpawnUnit (Vector3 point)
    {
        SpawnUnit(point, true);
    }

    public void SpawnUnit (Vector3 point, bool nextTurn)
    {
        GameObject spawnUnit = Instantiate(selectSoldier.unitSoldier.objectMesh, point, Quaternion.identity) as GameObject;
        spawnUnit.AddComponent<Character>().init(selectSoldier.unitSoldier);

        spawnUnit.transform.parent = PlayerManager.instance.playerCurrentTurn.playerHome;
        PlayerManager.instance.playerCurrentTurn.playerGameObject.Add(spawnUnit);
        PlayerManager.instance.playerCurrentTurn.playerMoney -= selectSoldier.unitSoldier.cost;

        //PlayerManager.instance.playerRed[0].playerNormalStats.name = "hello";;
        selectSoldier = null;
        if (nextTurn)
            GameManager.instance.NextPlayerPrepare();
    }
}
