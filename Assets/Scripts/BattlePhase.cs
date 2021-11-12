using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class BattlePhase : MonoBehaviour {
    [HideInInspector]
    public GameManager gameManager;

    public Text currentTurnText;
    public LayerMask pawnLayer;
    private HashSet<GameObject> ColoredPawnObjects = new HashSet<GameObject>();
    private Pawn selectedPawn;

    public IEnumerator InitBattle() {
        currentTurnText.gameObject.SetActive(true);
        while (true) {
            yield return StartCoroutine(SelectPawn());
            yield return StartCoroutine(TakeControl());
            gameManager.NextTurn();
        }
    }

    private IEnumerator TakeControl() {
        selectedPawn.gameObject.GetComponentInChildren<MeshRenderer>().material.color /= 4;
        CharControler charControler = selectedPawn.gameObject.AddComponent<CharControler>();
        PawnCombat pawnCombat = selectedPawn.gameObject.AddComponent<PawnCombat>();
        pawnCombat.battlePhase = this;
        selectedPawn.GetComponentInChildren<Camera>().enabled = true;
        gameManager.camera.gameObject.SetActive(false);

        yield return new WaitForSeconds(5);
        print("returning");

        selectedPawn.GetComponentInChildren<Camera>().enabled = false;
        gameManager.camera.gameObject.SetActive(true);
        Destroy(pawnCombat);
        Destroy(charControler);
        selectedPawn = null;
        Cursor.visible = true;

        yield return null;
    }

    public void GuiUpdatePlayer() {
        currentTurnText.text = $"{gameManager.currentPlayer.name}, pawns left: {gameManager.currentPlayer.pawns.Count}";
        currentTurnText.color = gameManager.currentPlayer.color;
    }

    private IEnumerator SelectPawn() {
        while (!selectedPawn) {
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
                        selectedPawn = highlightedGameObject.GetComponentInParent<Pawn>();
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