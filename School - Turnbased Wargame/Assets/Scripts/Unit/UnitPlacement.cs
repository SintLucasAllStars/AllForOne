using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    [SerializeField] LayerMask placeLayer, InsideSpawnPlaceLayer;
    public GameObject selectObject;
    private bool selectIsInsidePlace;
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
                if (selectIsInsidePlace != (hit.collider.gameObject.layer == Mathf.Log(InsideSpawnPlaceLayer.value, 2)))
                {
                    selectIsInsidePlace = !selectIsInsidePlace;

                    foreach (MeshRenderer mr in selectObject.GetComponentsInChildren<MeshRenderer>())
                    {
                        mr.material.color = selectIsInsidePlace ? Color.green : Color.red;
                    }
                }
             
                selectObject.transform.position = hit.point;

                if (selectIsInsidePlace && Input.GetMouseButtonDown(0))
                {
                    Destroy(selectObject);
                    SpawnUnit(hit.point);
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
        Vector3 spawnHeight = new Vector3(0, 2, 0);
        GameObject spawnUnit = Instantiate(selectSoldier.unitSoldier.objectMesh, point + spawnHeight, Quaternion.identity) as GameObject;
        spawnUnit.AddComponent<Character>().init(selectSoldier.unitSoldier, GameManager.instance.isPlayerBlue, PlayerManager.instance.playerCurrentTurn.playerGameObject.Count);

        spawnUnit.transform.parent = PlayerManager.instance.playerCurrentTurn.playerHome;
        PlayerManager.instance.playerCurrentTurn.playerGameObject.Add(spawnUnit);
        PlayerManager.instance.playerCurrentTurn.playerMoney -= selectSoldier.unitSoldier.cost;

        //PlayerManager.instance.playerRed[0].playerNormalStats.name = "hello";;
        selectSoldier = null;
        if (nextTurn)
            GameControl.instance.NextPlayerPrepare();
    }
}
