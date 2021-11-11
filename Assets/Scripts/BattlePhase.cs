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
    private HashSet<GameObject> ColoredPawnObjects = new HashSet<GameObject>();
    private Pawn slectedPawn;

    public IEnumerator InitBattle() {
        currentTurnText.gameObject.SetActive(true);
        yield return StartCoroutine(SelectPawn());
        yield return StartCoroutine(TakeControl());
    }

    private IEnumerator TakeControl() {
        slectedPawn.gameObject.GetComponentInChildren<MeshRenderer>().material.color /= 4;
        yield return null;
    }

    public void GuiUpdatePlayer() {
        currentTurnText.text = gameManager.currentPlayer.name;
        currentTurnText.color = gameManager.currentPlayer.color;
    }

    private IEnumerator SelectPawn() {
        while (!slectedPawn) {
            Ray ray = gameManager.camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, pawnLayer)) {
                GameObject highlightedGameObject = hit.collider.gameObject;
                if (gameManager.currentPlayer == highlightedGameObject.GetComponentInParent<Pawn>().player) {
                    if (!ColoredPawnObjects.Contains(highlightedGameObject)) {
                        ColoredPawnObjects.Add(highlightedGameObject);
                        highlightedGameObject.GetComponent<MeshRenderer>().material.color /= 4;
                    }
                    // read mouse button
                    if (Input.GetMouseButtonDown(0)) {
                        print("selected");
                        slectedPawn = highlightedGameObject.GetComponentInParent<Pawn>();
                    }
                }
            }
            else {
                foreach (GameObject pawnObject in ColoredPawnObjects) {
                    pawnObject.GetComponent<MeshRenderer>().material.color =
                        pawnObject.GetComponentInParent<Pawn>().player.color;
                }

                ColoredPawnObjects.Clear();
            }


            yield return 0;
        }
    }
}