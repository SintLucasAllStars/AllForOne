using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattlePhase : MonoBehaviour {
    [HideInInspector]
    public GameManager gameManager;

    public Text currentTurnText;
    public LayerMask pawnLayer;
    private HashSet<GameObject> coloredPawns = new HashSet<GameObject>();

    public void InitBattle() {
        currentTurnText.gameObject.SetActive(true);
        StartCoroutine(SelectPawn());
    }

    public void GuiUpdatePlayer() {
        currentTurnText.text = gameManager.currentPlayer.name;
        currentTurnText.color = gameManager.currentPlayer.color;
    }

    private IEnumerator SelectPawn() {
        bool placed = false;
        while (!placed) {
            Ray ray = gameManager.camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, pawnLayer)) {
                GameObject highlitedGameobject = hit.collider.gameObject;
                if (!coloredPawns.Contains(highlitedGameobject)) {
                    coloredPawns.Add(highlitedGameobject);
                    highlitedGameobject.GetComponentInChildren<MeshRenderer>().material.color = Color.magenta;
                    if (Input.GetMouseButtonDown(0)) {
                        placed = true;
                    }
                }
            }
            else {
                foreach (GameObject pawnObject in coloredPawns) {
                        pawnObject.GetComponent<MeshRenderer>().material.color = pawnObject.GetComponentInParent<Pawn>().player.color;
                }
                coloredPawns.Clear();
            }


            yield return 0;
        }
    }
}