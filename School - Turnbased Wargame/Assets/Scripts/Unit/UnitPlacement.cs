using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    public UnitUIEvent canvasUIEvent;

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
                        GameObject spawnUnit = Instantiate(selectSoldier.unitSoldier.objectMesh, hit.point, Quaternion.identity) as GameObject;
                        spawnUnit.AddComponent<Character>().init(selectSoldier.unitSoldier);
                        PlayerManager.instance.playerRed.playerGameObject.Add(spawnUnit);

                        PlayerManager.instance.playerRed.playerMoney -= selectSoldier.unitSoldier.cost;

                        //PlayerManager.instance.playerRed[0].playerNormalStats.name = "hello";;
                        selectSoldier = null;
                        canvasUIEvent.NavigaTo(UnitUIEvent.CanvasNavigation.unitList);
                    }
                } else
                {

                }
            }
        }
    }
}
